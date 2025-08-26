using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float maxSpeed = 20;

    private Rigidbody rb;

    private Vector3 direction;
    private float vertical;
    private float horizontal;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        InputMovementPlayer();
    }

    private void FixedUpdate()
    {
        LogicMovement();
    }

    private void InputMovementPlayer()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0; right.y = 0;

        direction = forward * vertical + right * horizontal;
        direction.Normalize();
    }

    private void LogicMovement()
    {
        Vector3 horizontalVelocityRB = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (direction.sqrMagnitude > 0.1f) transform.forward = Vector3.Lerp(transform.forward, direction, speed * Time.fixedDeltaTime);

        if (horizontalVelocityRB.magnitude > maxSpeed)
        {
            Vector3 clampVelocity = horizontalVelocityRB.normalized * maxSpeed;
            rb.velocity = new Vector3(horizontalVelocityRB.x, rb.velocity.y, horizontalVelocityRB.x);

            return;
        }


        rb.AddForce(direction * (speed * Time.fixedDeltaTime), ForceMode.VelocityChange);
    }
}
