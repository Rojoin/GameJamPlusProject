using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float maxSpeed;
    public void Move(Vector2 moveInput)
    {
        if (moveInput.magnitude > 0.5f)
            rb.drag = 0;
        else
            rb.drag = 4;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        //rb.AddForce(move * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        //
        //if (rb.velocity.x > maxSpeed)
        //    rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
        //if (rb.velocity.z > maxSpeed)
        //    rb.velocity = new Vector3(rb.velocity.y, rb.velocity.y, maxSpeed);

        rb.velocity = move * moveSpeed * Time.deltaTime;
    }
}
