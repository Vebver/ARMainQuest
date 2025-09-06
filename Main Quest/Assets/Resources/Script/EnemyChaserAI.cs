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
    public float detectionRange = 8f;

    [Header("Drops")]
    public GameObject[] dropPrefabs; // Assign at least 3 prefabs in the Inspector
    public float dropChance = 0.3f; // 30% chance to drop an item

    private NavMeshAgent navAgent;
    private Animator animator;
    private GameObject player;
    private float lastAttackTime;
    private bool isDead = false;

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

                bool isMoving = navAgent.velocity.magnitude > 0.1f;
                if (animator != null)
                {
                    animator.SetBool("IsWalking", isMoving);
                }
            }
        }
        else
        {
            if (navAgent != null)
            {
                navAgent.ResetPath();
                if (animator != null)
                {
                    animator.SetBool("IsWalking", false);
                }
            }
        }
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

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        if (Random.Range(0f, 1f) < dropChance)
        {
            DropRandomItem();
        }

        Destroy(gameObject, 3f);
    }

    void DropRandomItem()
    {
        if (dropPrefabs != null && dropPrefabs.Length > 0)
        {
            int index = Random.Range(0, dropPrefabs.Length);
            Instantiate(dropPrefabs[index], transform.position, Quaternion.identity);
        }
    }
}