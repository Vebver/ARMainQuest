using UnityEngine;
using System.Collections;

public class PlayerBuffs : MonoBehaviour
{
    public float attack = 10f;
    public float defense = 5f;
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [Header("References")]
    private Animator animator;
    private bool isDead = false;

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
        maxHealth += amount;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        // Optionally, you can revert maxHealth after duration if you want it temporary
    }

    private IEnumerator BuffRoutine(System.Action apply, System.Action revert, float duration)
    {
        apply();
        yield return new WaitForSeconds(duration);
        revert();
    }
    public void TakeDamage(float amount)
    {
        if (isDead) return; // stop taking damage when dead

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
            animator.SetTrigger("Death"); // 🔹 Play death animation
        }

        // Optional: disable movement scripts
        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null) controller.enabled = false;
    }
}
