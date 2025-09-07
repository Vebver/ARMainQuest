using UnityEngine;
using UnityEngine.AI;

public class EnemyChaserAI : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 100f;
    public float attackDamage = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float detectionRange = 8f; // Enemy will only chase player within this range

    [Header("Drops")]
    public GameObject[] dropPrefabs; // Assign buff prefabs here
    public float dropChance = 0.3f;

    private NavMeshAgent navAgent;
    private Animator animator;
    private GameObject player;
    private bool isDead = false;
    private float lastAttackTime = -Mathf.Infinity;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        if (navAgent != null)
        {
            navAgent.speed = moveSpeed;
            navAgent.stoppingDistance = attackRange;
        }
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (navAgent != null && navAgent.enabled)
            {
                navAgent.SetDestination(player.transform.position);

                if (animator != null)
                    animator.SetBool("IsWalking", navAgent.velocity.magnitude > 0.1f);
            }

            // Attack if close enough and cooldown has passed
            if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
            }
        }
        else
        {
            if (navAgent != null)
            {
                navAgent.ResetPath();
                if (animator != null)
                    animator.SetBool("IsWalking", false);
            }
        }
    }

    void AttackPlayer()
    {
        lastAttackTime = Time.time;

        // Play attack animation if available
        if (animator != null)
            animator.SetTrigger("Attack");

        // Deal damage to player
        PlayerBuffs buffs = player.GetComponent<PlayerBuffs>();
        if (buffs != null)
        {
            buffs.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;

        if (navAgent != null)
            navAgent.enabled = false;

        if (animator != null)
            animator.SetTrigger("Death");

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        if (dropPrefabs != null && dropPrefabs.Length > 0 && Random.value < dropChance)
        {
            int index = Random.Range(0, dropPrefabs.Length);
            Instantiate(dropPrefabs[index], transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 3f);
    }
}
