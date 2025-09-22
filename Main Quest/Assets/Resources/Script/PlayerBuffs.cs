using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerBuffs : MonoBehaviour
{
    public float attack = 10f;
    public float defense = 5f;
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [Header("References")]
    private Animator animator;
    private bool isDead = false;

    [Header("Attack Settings")]
    public float attackRange = 2f;
    public LayerMask enemyLayer;
    public float attackCooldown = 1f;  // seconds
    private float lastAttackTime = -Mathf.Infinity;

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // finds animator on child (e.g., Alex)
        currentHealth = maxHealth;
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
        StartCoroutine(BuffRoutine(() => maxHealth += amount, () => maxHealth -= amount, duration));
    }


    private IEnumerator BuffRoutine(System.Action apply, System.Action revert, float duration)
    {
        apply();
        yield return new WaitForSeconds(duration);
        revert();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
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
        {
            animator.SetTrigger("Death");
        }

        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null) controller.enabled = false;
        SceneManager.LoadScene(0);
    }

    // 🔹 Call this method when Attack button is pressed
    public void Attack()
    {
        if (isDead) return;
        if (Time.time < lastAttackTime + attackCooldown) return; // cooldown check
        lastAttackTime = Time.time;

        if (animator != null)
            animator.SetTrigger("Attack");

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyChaserAI enemyAI = enemy.GetComponent<EnemyChaserAI>();
            if (enemyAI != null)
            {
                enemyAI.TakeDamage(attack);
                Debug.Log("Hit " + enemy.name + " for " + attack + " damage.");
            }
        }
    }

    // Just for visualization in Unity editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
