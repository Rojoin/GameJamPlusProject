using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private BoolChannelSO dashChannelSO;
    [SerializeField] private float moveSpeed;

    private Vector2 dashInput;

    private bool dashing = false;

    private float currentDashTimer = 0.0f;
    private float maxDashTimer = 0.4f;

    private void FixedUpdate()
    {
        if(dashing)
        {
            Debug.Log(5);

            currentDashTimer += Time.fixedDeltaTime;

            if(currentDashTimer >= maxDashTimer)
            {
                dashing = false;
                currentDashTimer = 0;
                dashChannelSO.RaiseEvent(false);
            }
            else
            {
                Dash();
            }
        }
    }

    public void Move(Vector2 moveInput)
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        cc.SimpleMove(move * moveSpeed * Time.fixedDeltaTime);
    }

    public void StartDash(Vector2 moveInput)
    {
        Debug.Log(4);
        dashing = true;
        dashInput = moveInput;
        dashChannelSO.RaiseEvent(true);
    }

    private void Dash()
    {
        Debug.Log(6);

        Vector3 move = transform.right * dashInput.x + transform.forward * dashInput.y;

        cc.Move(move * (moveSpeed*2) * Time.fixedDeltaTime);
    }
}
