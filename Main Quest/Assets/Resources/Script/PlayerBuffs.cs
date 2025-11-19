using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerBuffs : MonoBehaviour
{
    [Header("Player Stats")]
    public float attack = 10f;
    public float defense = 5f;
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [Header("References")]
    public GameObject alex; // Assign Alex (child with Animator) in Inspector
    public Healthbar healthbar;

    private Animator animator;
    private bool isDead = false;

    [Header("Attack Settings")]
    public float attackRange = 2f;
    public LayerMask enemyLayer;
    public float attackCooldown = 1f;
    private float lastAttackTime = -Mathf.Infinity;

    void Start()
    {
        if (alex != null)
        {
            animator = alex.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("⚠️ Alex reference not assigned in PlayerBuffs!");
        }

        currentHealth = maxHealth;

        if (healthbar != null)
            healthbar.UpdateHealthbar(maxHealth, currentHealth);
    }

    void Update()
    {
        if (isDead) return;

        // 🖱️ Right-click triggers attack
        if (Input.GetMouseButtonDown(1) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    public void Attack()
    {
        lastAttackTime = Time.time;

        if (animator != null)
            animator.SetTrigger("Attack");

        Vector3 direction = alex != null ? alex.transform.forward : transform.forward;
        Debug.DrawRay(transform.position, direction * attackRange, Color.red, 1f);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, attackRange, enemyLayer))
        {
            EnemyChaserAI enemyAI = hit.collider.GetComponentInParent<EnemyChaserAI>();
            if (enemyAI != null)
            {
                enemyAI.TakeDamage(attack);
                Debug.Log("Raycast hit " + hit.collider.name + " for " + attack + " damage.");
            }
            else
            {
                Debug.Log("Hit object has no EnemyChaserAI.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any enemy.");
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        float damageTaken = Mathf.Max(amount - defense, 1);
        currentHealth -= damageTaken;

        if (healthbar != null)
            healthbar.UpdateHealthbar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player died!");

        if (animator != null)
            animator.SetTrigger("Death");

        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null) controller.enabled = false;

        SceneManager.LoadScene(7);
    }

    public void ApplyAttackBuff(float amount, float duration)
    {
        StartCoroutine(BuffRoutine(() => attack += amount, () => attack -= amount, duration));
    }

    public void ApplyDefenseBuff(float amount, float duration)
    {
        StartCoroutine(BuffRoutine(() => defense += amount, () => defense -= amount, duration));
    }

    public void ApplyHealthBuff(float amount, float duration)
    {
        StartCoroutine(BuffRoutine(() =>
        {
            maxHealth += amount;
            currentHealth += amount;
            healthbar.UpdateHealthbar(maxHealth, currentHealth);
        },
        () =>
        {
            maxHealth -= amount;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            healthbar.UpdateHealthbar(maxHealth, currentHealth);
        },
        duration));
    }

    private IEnumerator BuffRoutine(System.Action apply, System.Action revert, float duration)
    {
        apply();
        yield return new WaitForSeconds(duration);
        revert();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}