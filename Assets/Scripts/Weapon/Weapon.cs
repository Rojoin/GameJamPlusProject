using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private static readonly int Shoot1 = Animator.StringToHash("Shoot");
    [SerializeField] LayerMask targetLayer;
    [SerializeField] AudioSource audioSource;
    public Animator animator;

    public void Shoot(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = new Ray(origin, direction);
        //animator?.SetTrigger(Shoot1);

        audioSource.Play();

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
        {
            hit.collider.GetComponent<IDamageable>()?.RecieveDamage(1);
        }
    }
}
