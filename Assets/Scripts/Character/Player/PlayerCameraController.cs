using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100.0f;

    [SerializeField] private Transform playerBody;

    private float xRotation = 0.0f;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UpdateCamera(Vector2 lookValue)
    {
        lookValue = new Vector2(lookValue.x * mouseSensitivity * Time.deltaTime,
                                lookValue.y * mouseSensitivity * Time.deltaTime);
        
        xRotation -= lookValue.y;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * lookValue.x);
    }
}
