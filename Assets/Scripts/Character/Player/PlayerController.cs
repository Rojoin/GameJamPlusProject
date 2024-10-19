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
    [SerializeField] private BoolChannelSO toggleCameraBO;
    [SerializeField] private BoolChannelSO toggleWeaponBO;
    [SerializeField] private BoolChannelSO toggleMovementBO;
    [SerializeField] private BoolChannelSO toggleDashBO;

    private bool toggleCamera = true;
    private bool toggleMovement = true;

    private Vector2 lastInput = Vector2.zero;

    private void Awake()
    {
        toggleCameraBO.Subscribe(ToggleCamera);
        toggleMovementBO.Subscribe(ToggleMovement);
    }

    private void OnDestroy()
    {
        toggleCameraBO.Unsubscribe(ToggleCamera);
        toggleMovementBO.Unsubscribe(ToggleMovement);
    }

    private void Update()
    {
        playerCharacter.Move(lastInput);
    }

    public void OnMovement(InputValue movement)
    {
        if(toggleMovement)
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
    }

    public void OnCameraLook(InputValue look)
    {
        if(toggleCamera)
        {
            if (look.Get() != null)
            {
                cameraController.UpdateCamera((Vector2)look.Get());
            }
        }
    }

    public void ToggleCamera(bool value) => toggleCamera = value;
    public void ToggleMovement(bool value) => toggleMovement = value;
}
