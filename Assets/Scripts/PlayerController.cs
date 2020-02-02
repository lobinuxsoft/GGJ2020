using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject bobCamera;
    CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float speed = 6.0f;
    private Animator animator;
    public Vector3 startPosition;
    enum FSM
    {
        Idle,
        Movement,
        Repairing,
        count
    }

    private FSM fsm = FSM.Idle;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        //animator.SetTrigger(Tags.idleTrigger);
        startPosition = transform.position;
    }

    void Update()
    {

        if (gameState.currentEntity != GameState.PlayableEntities.Bob)
        {
            bobCamera.SetActive(false);
            transform.position = startPosition;
            return;
        }
        else
        {
            bobCamera.SetActive(true);
        }

        moveDirection = new Vector3(Input.GetAxis(Tags.horizontalAxis), 0.0f, Input.GetAxis(Tags.verticalAxis));
        moveDirection *= speed;

        switch (fsm)
        {
            case FSM.Idle:

                if (moveDirection != Vector3.zero)
                {
                    animator.SetFloat(Tags.runTrigger, 1);
                    fsm = FSM.Movement;
                    return;

                }
                break;

            case FSM.Movement:

                if (moveDirection == Vector3.zero)
                {
                    animator.SetFloat(Tags.runTrigger, 0);
                    fsm = FSM.Idle;

                    if (Random.Range(0.0f, 1.0f) < 0.4f)
                        AudioManager.instance.PlayAudio(AudioManager.instance.playerAudio, AudioKey.Idle);

                    return;
                }

                characterController.Move(moveDirection * Time.deltaTime);
                transform.LookAt(transform.position + new Vector3(moveDirection.x, 0.0f, moveDirection.z));
                break;
            case FSM.Repairing:
                animator.SetBool(Tags.repairTrigger, true);
                break;
        }

    }

    private void EnableMovement()
    {
        fsm = FSM.Idle;
        UIManager.instance.TriggerRepairProgress();
        AudioManager.instance.PlayAudio(AudioManager.instance.playerAudio, AudioKey.FireStopped);
        animator.SetBool(Tags.repairTrigger, false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.repairObjectTag))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                BreakableObject breakableObject = other.GetComponent<BreakableObject>();
                breakableObject.InReparation();
                Invoke("EnableMovement", breakableObject.reparationTime);
                AudioManager.instance.PlayAudio(AudioManager.instance.playerAudio, AudioKey.Repair);
                UIManager.instance.TriggerRepairProgress();
                fsm = FSM.Repairing;
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.BobCabinTag) && gameState.currentEntity == GameState.PlayableEntities.Bob)
        {
            gameState.currentEntity = GameState.PlayableEntities.Ship;
            AudioManager.instance.PlayAudio(AudioManager.instance.SFXAudio, AudioKey.Door);
        }
    }

}
