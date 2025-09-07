using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 100f;
    public float attackDamage = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Drops")]
    public GameObject energyOrbPrefab; // Assign your EnergyOrb prefab here
    public float energyDropChance = 0.3f; // 30% chance to drop energy

    private NavMeshAgent navAgent;
    private Animator animator;
    private GameObject Player;
    private float lastAttackTime;
    private bool isDead = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Find Player
        Player = GameObject.FindWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("Player not found! Make sure to tag your player GameObject as 'Player'");
        }

        // Setup NavMeshAgent
        if (navAgent != null)
        {
            navAgent.speed = moveSpeed;
            navAgent.stoppingDistance = attackRange;
        }
    }

    void Update()
    {
        if (isDead || Player == null) return;

        if (navAgent != null && navAgent.enabled)
        {
            navAgent.SetDestination(Player.transform.position);

            // Walk animation
            bool isMoving = navAgent.velocity.magnitude > 0.1f;
            animator.SetBool("IsWalking", isMoving);

            // Attack when close
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
            }
        }
    }

    void AttackPlayer()
    {
        lastAttackTime = Time.time;

        // Stop moving when attacking
        if (navAgent != null)
        {
            navAgent.ResetPath();
        }

        // Play attack animation
        animator.SetTrigger("Attack");
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (navAgent != null)
        {
            navAgent.enabled = false;
        }

        animator.SetTrigger("Death");

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        if (Random.Range(0f, 1f) < energyDropChance)
        {
            DropEnergyOrb();
        }

        Destroy(gameObject, 3f);
    }

    void DropEnergyOrb()
    {
        if (energyOrbPrefab != null)
        {
            Instantiate(energyOrbPrefab, transform.position, Quaternion.identity);
        }
    }
}
