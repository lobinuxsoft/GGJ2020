using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string horizontalAxis = "Horizontal";
    private const string verticalAxis = "Vertical";

    public Vector3 movementForce;
    private Rigidbody rb;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float friction;
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw(horizontalAxis);
        float verticalMovement = Input.GetAxisRaw(verticalAxis);

        if (horizontalMovement != 0.0f)
            movementForce.x = horizontalMovement * movementSpeed;
        else if (movementForce.x > 0.0f)
            movementForce.x -= Time.deltaTime * friction;
        else if (movementForce.x < 0.0f)
            movementForce.x += Time.deltaTime * friction;

        if (Mathf.Abs(movementForce.x) < 0.2f)
        {
            movementForce.x = 0.0f;
        }



        if (verticalMovement != 0.0f)
            movementForce.z = verticalMovement * movementSpeed;
        else if (movementForce.z > 0.0f)
            movementForce.z -= Time.deltaTime * friction;
        else if (movementForce.z < 0.0f)
            movementForce.z += Time.deltaTime * friction;

        if (Mathf.Abs(movementForce.z) < Time.unscaledDeltaTime * friction)
        {
            movementForce.z = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movementForce;
        if (movementForce != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, movementForce.normalized, 1.0f);
        }
    }

}
