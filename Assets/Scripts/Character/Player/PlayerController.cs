using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Set Up Variables")] [SerializeField]
    private PlayerCameraController cameraController;

    [SerializeField] private Character playerCharacter;
    [SerializeField] private Weapon playerWeapon;
    [SerializeField] private float maxHealth;
    private float currentHealth;

    [Header("Event Channels")] [SerializeField]
    private GameObjectChannelSO playerRefGO;

    [SerializeField] private BoolChannelSO toggleCameraBO;
    [SerializeField] private BoolChannelSO toggleWeaponBO;
    [SerializeField] private BoolChannelSO toggleMovementBO;
    [SerializeField] private BoolChannelSO toggleDashBO;
    [SerializeField] private BoolChannelSO dashStartChannelBO;
    [SerializeField] private FloatChannelSO healthChangedChannelSO;
    [SerializeField] private VoidChannelSO deathChannelSO;
    [SerializeField] private BoolChannelSO toggleHudInteractable;

    private bool toggleCamera = true;
    private bool toggleMovement = true;
    private bool toggleWeapon = true;
    private bool toggleDash = true;
    private bool dashStarted = false;

    private Vector2 lastInput = Vector2.zero;

    private void Awake()
    {
        toggleCameraBO?.Subscribe(ToggleCamera);
        toggleMovementBO?.Subscribe(ToggleMovement);
        toggleWeaponBO?.Subscribe(ToggleWeapon);
        toggleDashBO?.Subscribe(ToggleDash);
        dashStartChannelBO?.Subscribe(ToggleDashStarted);

        Cursor.visible = false;
        playerRefGO.RaiseEvent(gameObject);
        HealDamage(maxHealth);
    }

    private void OnDestroy()
    {
        toggleCameraBO?.Unsubscribe(ToggleCamera);
        toggleMovementBO?.Unsubscribe(ToggleMovement);
        toggleWeaponBO?.Unsubscribe(ToggleWeapon);
        toggleDashBO?.Unsubscribe(ToggleDash);
        dashStartChannelBO?.Unsubscribe(ToggleDashStarted);
    }

    private void FixedUpdate()
    {
        playerCharacter.Move(lastInput);
    }

    public void OnMovement(InputValue movement)
    {
        if (toggleMovement)
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
        if (toggleCamera)
        {
            if (look.Get() != null)
            {
                cameraController.UpdateCamera((Vector2)look.Get());
            }
        }
    }

    public void OnShoot(InputValue shoot)
    {
        if (toggleWeapon)
        {
            if (shoot.Get() != null)
            {
                Vector3 origin = cameraController.transform.position;
                Vector3 direction = cameraController.transform.forward;
                playerWeapon.Shoot(origin, direction);
            }
        }
    }

    public void OnDash(InputValue dash)
    {
        if (toggleDash)
        {
            if (dash.Get() != null && dashStarted == false)
            {
                Vector2 dashInput;
                if (lastInput == Vector2.zero)
                    dashInput = Vector2.up;
                else
                    dashInput = lastInput;

                playerCharacter.StartDash(dashInput);
            }
        }
    }

    public void RecieveDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            deathChannelSO?.RaiseEvent();
        }

        float health = currentHealth / maxHealth;
        Debug.Log($"The player healthbar should be: {health}");
        healthChangedChannelSO?.RaiseEvent(health);
    }

    public void HealDamage(float heal)
    {
        currentHealth += heal;
    }

    public void Die()
    {
    }

    public void ToggleCamera(bool value)
    {
        toggleCamera = value;
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !value;
        toggleHudInteractable.RaiseEvent(!value);
        toggleWeaponBO.RaiseEvent(value);
    }

    public void OnToggleCamera()
    {
        toggleCameraBO.RaiseEvent(!toggleCamera);
    }

    public void ToggleMovement(bool value) => toggleMovement = value;
    public void ToggleWeapon(bool value) => toggleWeapon = value;
    public void ToggleDash(bool value) => toggleDash = value;
    public void ToggleDashStarted(bool value) => dashStarted = value;
}