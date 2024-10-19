using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCameraController cameraController;
    [SerializeField] private Rigidbody rb;

    public void OnMovement(InputValue movement)
    {
        if(movement.Get() != null)
        {
            Vector2 value = (Vector2)movement.Get();

            rb.AddForce(new Vector3(transform.right.x + value.x, 0, transform.forward.z + value.y) * 50);
            Debug.Log(value);
        }
    }

    public void OnCameraLook(InputValue look)
    {
        if(look.Get() != null)
        {
            cameraController.UpdateCamera((Vector2)look.Get());
        }
    }
}
