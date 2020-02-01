using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float gravity = 20.0f;

    enum FSM
    {
        Movement,
        Repairing,
        count
    }

    private FSM fsm = FSM.Movement;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        switch (fsm)
        {
            case FSM.Movement:
                if (characterController.isGrounded)
                {
                    moveDirection = new Vector3(Input.GetAxis(Tags.horizontalAxis), 0.0f, Input.GetAxis(Tags.verticalAxis));
                    moveDirection *= speed;
                }

                moveDirection.y -= gravity * Time.deltaTime;

                characterController.Move(moveDirection * Time.deltaTime);
                transform.LookAt(transform.position + new Vector3(moveDirection.x, 0.0f, moveDirection.z));
                break;
            case FSM.Repairing:
                break;
        }

    }

    private void EnableMovement()
    {
        fsm = FSM.Movement;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.repairObjectTag))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("puto");
                BreakableObject breakableObject = other.GetComponent<BreakableObject>();
                breakableObject.InReparation();
                Invoke("EnableMovement", breakableObject.reparationTime);
                fsm = FSM.Repairing;
            }
        }
    }
}
