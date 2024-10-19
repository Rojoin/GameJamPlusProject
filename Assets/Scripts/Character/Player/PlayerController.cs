using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Set Up Variables")]
    [SerializeField] private PlayerCameraController cameraController;
    [SerializeField] private Character playerCharacter;

    [Header("Event Channels")]
    [SerializeField] private BoolChannelSO toggleCamera;
    [SerializeField] private BoolChannelSO toggleWeapon;
    [SerializeField] private BoolChannelSO toggleMovement;
    [SerializeField] private BoolChannelSO toggleDash;

    Vector2 lastInput = Vector2.zero;

    private void Update()
    {
        playerCharacter.Move(lastInput);
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
