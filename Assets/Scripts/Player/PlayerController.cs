using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCameraController cameraController;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed;
    Vector2 lastInput = Vector2.zero;

    private void Update()
    {
        //Brackeys ahhh movement
        Vector3 move = transform.right * lastInput.x + transform.forward * lastInput.y;

        rb.velocity = move * speed * Time.deltaTime;
    }

    public void OnMovement(InputValue movement)
    {
        if (movement.Get() != null)
        {
            Vector2 value = (Vector2)movement.Get();
            lastInput = value;
        }
        else
        {
            lastInput = Vector2.zero;
        }
    }

    public void OnCameraLook(InputValue look)
    {
        if (look.Get() != null)
        {
            cameraController.UpdateCamera((Vector2)look.Get());
        }
    }
}
