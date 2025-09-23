using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 500;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    void Die()
    {
        EnemyManager.Instance.BossDefeated();
        Destroy(gameObject);
    }
}
