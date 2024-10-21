using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] AudioClip hitAudioClip;
    [SerializeField] AudioClip deathAudioClip;

    [SerializeField] private FloatChannelSO recieveDamageChannelSO;
    [SerializeField] private VoidChannelSO deathChannelSO;
    [SerializeField] private float currentHealth;
    [SerializeField] private float  damage= 5;

    private float maxTimer = 3.0f;
    private float currentTimer = 0.0f;

    private void Update()
    {
        if (player != null)
        {
            if (currentTimer == 0)
                animator.SetBool("Attacking", false);

            agent.destination = player.position;

            animator.SetBool("Walking", true);

            AttackTimer();
        }

    }

    private void AttackTimer()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer > maxTimer)
        {
            animator.SetBool("Attacking", true);
            TryAttack();
            currentTimer = 0;
        }
    }

    private void TryAttack()
    {
        var colliders = Physics.OverlapSphere(transform.position, 4.0f, playerLayer);

        if (colliders.Length > 0)
        {
            Debug.Log("Attacked player");
            colliders[0].GetComponent<IDamageable>().RecieveDamage(damage);
        }
        else
            Debug.Log("Missed attack");
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void RecieveDamage(float damage)
    {
        SoundManager.Instance?.PlaySound(hitAudioClip);

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            recieveDamageChannelSO?.RaiseEvent(currentHealth);
        }
    }

    public void HealDamage(float heal)
    {
        currentHealth += heal;
    }

    public void Die()
    {
        deathChannelSO?.RaiseEvent();
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        SoundManager.Instance?.PlaySound(deathAudioClip);

        capsuleCollider.enabled = false;
        animator.SetTrigger("Death");
        player = null;
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
