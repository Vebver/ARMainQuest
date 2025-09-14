using System.Collections;
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

    [Header("UI Drops")]
    public Transform uiCanvas;

    [Header("Drops")]
    public GameObject[] dropPrefabs;
    public float dropChance = 1f;

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

        // disable ragdoll at start
        SetRagdollActive(false);
        MobManager.Instance?.RegisterMob();

    }

    // Called from an Animation Event during the attack animation
    public void DealDamage()
    {
        if (isDead || player == null) return;

        PlayerBuffs buffs = player.GetComponent<PlayerBuffs>();
        if (buffs != null)
        {
            buffs.TakeDamage(attackDamage);
            Debug.Log($"{name} attacked {player.name} for {attackDamage} damage!");
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

        if (animator != null)
            animator.SetTrigger("Attack");

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
        {
            Die();
        }
    }

    void TryDropItem()
    {
        if (dropPrefabs == null || dropPrefabs.Length == 0) return;

        int index = Random.Range(0, dropPrefabs.Length);

        Canvas worldCanvas = FindObjectOfType<Canvas>();
        if (worldCanvas == null)
        {
            Debug.LogError("⚠️ No Canvas found! Please add a World-Space Canvas to the scene.");
            return;
        }

        GameObject drop = Instantiate(dropPrefabs[index], worldCanvas.transform);

        // 🔹 Position above enemy death spot
        float heightOffset = 0.5f;
        drop.transform.position = transform.position + Vector3.up * heightOffset;

        // 🔹 Force Y rotation
        drop.transform.rotation = Quaternion.Euler(180f, 0f, 0f);

        // 🔹 Apply scale
        drop.transform.localScale = new Vector3(-0.00567931452f, -0.00534023503f, 0.572300029f);

        // Optional UI sizing
        RectTransform rt = drop.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.sizeDelta = new Vector2(100, 100);
        }

        // 🔹 Add CanvasGroup for fading
        CanvasGroup cg = drop.GetComponent<CanvasGroup>();
        if (cg == null) cg = drop.AddComponent<CanvasGroup>();
        cg.alpha = 0f; // start invisible

        // Start fade-in
        StartCoroutine(FadeInAndRotate(drop, cg, 0.5f, 90f));
    }

    IEnumerator FadeInAndRotate(GameObject drop, CanvasGroup cg, float fadeDuration, float rotationSpeed)
    {
        // Fade in first
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            if (cg == null || drop == null)
                yield break;

            cg.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        if (cg != null)
            cg.alpha = 1f;

        // 🔁 Continuous rotation
        while (drop != null)
        {
            drop.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

            // Optional: check if CanvasGroup still exists
            if (cg == null)
                yield break;

            yield return null;
        }
    }



    void Die()
    {
        isDead = true;

        if (navAgent != null)
            navAgent.enabled = false;

        // 🔹 Play Death animation
        if (animator != null)
        {
            animator.ResetTrigger("Attack");   // stop pending attack anims
            animator.SetBool("IsWalking", false);
            animator.SetTrigger("Death");      // play Death trigger
        }

        // Disable main collider so player can’t keep hitting
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        // 🔹 Roll drop chance
        TryDropItem();

        MobManager.Instance?.MobDefeated();

        // 🔹 Wait for Death animation before ragdoll
        Invoke(nameof(EnableRagdoll), 2.5f); // match death animation length
    }



    void EnableRagdoll()
    {
        if (animator != null)
            animator.enabled = false; // stop controlling bones

        SetRagdollActive(true);

        Destroy(gameObject, 5f); // cleanup after 5s
    }

    void SetRagdollActive(bool active)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (var rb in bodies)
        {
            if (rb != null && rb.gameObject != this.gameObject)
                rb.isKinematic = !active;
        }

        foreach (var c in colliders)
        {
            if (c != null && c.gameObject != this.gameObject)
                c.enabled = active;
        }
    }
}
