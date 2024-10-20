using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private float moveSpeed;

    public void Move(Vector2 moveInput)
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        cc.SimpleMove(move * moveSpeed * Time.fixedDeltaTime);
    }
}
