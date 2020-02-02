using System;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public class SheepMovement : MonoBehaviour
{
    [SerializeField] private Transform shipTransform;
    [SerializeField] private Transform shipModel;

    [Header("Settings")]
    public bool joystick = true;

    [Space] [Header("Parameters")]
    [SerializeField] private GameState gameState;
    [SerializeField] private ShipParameters shipParameters;

    [Space]

    [Header("Public References")]
    public Transform aimTarget;
    public CinemachineDollyCart dolly;
    public Transform cameraParent;

    [Space]

    [Header("Particles")]
    public ParticleSystem[] trail;
    public ParticleSystem[] circle;
    public ParticleSystem barrel;
    public ParticleSystem stars;

    [Space]
    
    [Header("Private Expose Reference")]
    [SerializeField] private GameObject flyCam;
    [SerializeField] private GameObject introCam;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        SetSpeed(shipParameters.forwardSpeed);

        if (gameState.currentEntity != GameState.PlayableEntities.Ship)
        {
            flyCam.SetActive(false);
            mainCamera.gameObject.SetActive(false);
            return;
        }
        else
        {
            mainCamera.gameObject.SetActive(true);
            flyCam.SetActive(true);
        }
        
        float h = joystick ? Input.GetAxis("Horizontal") : Input.GetAxis("Mouse X");
        float v = joystick ? Input.GetAxis("Vertical") : Input.GetAxis("Mouse Y");

        LocalMove(h, v, shipParameters.moveSpeed);
        RotationLook(h * shipParameters.maxLookInclination, v * shipParameters.maxLookInclination, shipParameters.lookSpeed);
        HorizontalLean(shipModel, h, 80, .1f);

        if (Input.GetButtonDown("Action") || Input.GetKeyDown(KeyCode.Space))
            Boost(true);

        if (Input.GetButtonUp("Action") || Input.GetKeyUp(KeyCode.Space))
            Boost(false);

        if (Input.GetButtonDown("Fire3"))
            Break(true);

        if (Input.GetButtonUp("Fire3"))
            Break(false);

        if (Input.GetButtonDown("TriggerL") || Input.GetButtonDown("TriggerR"))
        {
            int dir = Input.GetButtonDown("TriggerL") ? -1 : 1;
            QuickSpin(dir);
        }

        if (Input.GetButtonDown("ChangeState") || Input.GetKeyDown(KeyCode.Z))
            gameState.currentEntity = GameState.PlayableEntities.Bob;
    }

    void LocalMove(float x, float y, float speed)
    {
        shipTransform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(shipTransform.position);
        pos.x = Mathf.Clamp(pos.x, shipParameters.clampMovement, 1 - shipParameters.clampMovement);
        pos.y = Mathf.Clamp(pos.y, shipParameters.clampMovement, 1 - shipParameters.clampMovement);
        shipTransform.position = mainCamera.ViewportToWorldPoint(pos);
    }

    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        shipTransform.rotation = Quaternion.RotateTowards(shipTransform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);

    }

    public void QuickSpin(int dir)
    {
        if (!DOTween.IsTweening(shipModel))
        {
            shipModel.DOLocalRotate(new Vector3(shipModel.localEulerAngles.x, shipModel.localEulerAngles.y, 360 * -dir), .4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
            barrel.Play();
        }
    }

    void SetSpeed(float x)
    {
        dolly.m_Speed = x;
    }

    void SetCameraZoom(float zoom, float duration)
    {
        cameraParent.DOLocalMove(new Vector3(0, 0, zoom), duration);
    }

    void DistortionAmount(float x)
    {
        mainCamera.GetComponent<PostProcessVolume>().profile.GetSetting<LensDistortion>().intensity.value = x;
    }

    void FieldOfView(float fov)
    {
        cameraParent.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov;
    }

    void Chromatic(float x)
    {
        mainCamera.GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>().intensity.value = x;
    }


    void Boost(bool state)
    {

        if (state)
        {
            cameraParent.GetComponentInChildren<CinemachineImpulseSource>().GenerateImpulse();
            
            foreach (var t in trail)
            {
                t.Play();
            }

            foreach (var c in circle)
            {
                c.Play();
            }
        }
        else
        {
            foreach (var t in trail)
            {
                t.Stop();
            }
            
            foreach (var c in circle)
            {
                c.Stop();
            }
        }
        
        foreach (var t in trail)
        {
            t.GetComponent<TrailRenderer>().emitting = state;
        }
        //trail.GetComponent<TrailRenderer>().emitting = state;

        float origFov = state ? 40 : 55;
        float endFov = state ? 55 : 40;
        float origChrom = state ? 0 : 1;
        float endChrom = state ? 1 : 0;
        float origDistortion = state ? 0 : -30;
        float endDistorton = state ? -30 : 0;
        float starsVel = state ? -20 : -1;
        float speed = state ? shipParameters.forwardSpeed * 2 : shipParameters.forwardSpeed;
        float zoom = state ? -7 : 0;

        DOVirtual.Float(origChrom, endChrom, .5f, Chromatic);
        DOVirtual.Float(origFov, endFov, .5f, FieldOfView);
        DOVirtual.Float(origDistortion, endDistorton, .5f, DistortionAmount);
        var pvel = stars.velocityOverLifetime;
        pvel.z = starsVel;

        DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }

    void Break(bool state)
    {
        float speed = state ? shipParameters.forwardSpeed / 3 : shipParameters.forwardSpeed;
        float zoom = state ? 3 : 0;

        DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }

    private void OnDestroy()
    {
        gameState.currentEntity = GameState.PlayableEntities.Ship;
    }
}
