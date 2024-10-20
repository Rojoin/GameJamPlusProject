using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void RecieveDamage(int damage);
    public void HealDamage(int heal);
    public void Die();
}
