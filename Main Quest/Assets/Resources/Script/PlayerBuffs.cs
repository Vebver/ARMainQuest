using UnityEngine;
using System.Collections;

public class PlayerBuffs : MonoBehaviour
{
    public float attack = 10f;
    public float defense = 5f;
    public float maxHealth = 100f;
    public float currentHealth = 100f;

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
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // Optionally, handle player death here
            Debug.Log("Player died!");
        }
    }
}