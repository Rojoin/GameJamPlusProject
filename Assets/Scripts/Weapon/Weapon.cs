using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;

    public void Shoot(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = new Ray(origin, direction);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
        {
            hit.collider.GetComponent<IDamageable>()?.RecieveDamage(1);
        }
    }
}
