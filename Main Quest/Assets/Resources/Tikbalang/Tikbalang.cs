using UnityEngine;

public class Tikbalang : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    void Die()
    {
        EnemyManager.Instance.MobDefeated();
        Destroy(gameObject);
    }
}
