using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void RecieveDamage(float damage);
    public void HealDamage(float heal);
    public void Die();
}
