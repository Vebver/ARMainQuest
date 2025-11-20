using UnityEngine;
using UnityEngine.SceneManagement;

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
        // Notify the EnemyManager
        EnemyManager.Instance.BossDefeated();

        // 🔓 Unlock Visayas by marking Level 3 as completed
        PlayerPrefs.SetInt("Level2Completed", 1);
        PlayerPrefs.Save();
        Debug.Log("✅ Level 3 completed. Visayas region unlocked!");

        // Destroy boss object
        Destroy(gameObject);

        // 🔁 Load Visayas scene (Scene index 6 assumed)
        SceneManager.LoadScene("Main Menu"); // Replace with actual name
    }
}