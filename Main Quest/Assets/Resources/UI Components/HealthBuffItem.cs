using UnityEngine;

public class HealthBuffItem : MonoBehaviour
{
    public float collectDistance = 2f; // how close player must be
    public float healthBuffAmount = 5f;
    public float buffDuration = 10f;   // seconds (you can set to 0 if you want permanent)

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Check distance
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= collectDistance)
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        Debug.Log("✅ Player collected Health Buff: " + gameObject.name);

        // Apply +5 health buff
        PlayerBuffs buffs = player.GetComponent<PlayerBuffs>();
        if (buffs != null)
        {
            buffs.ApplyHealthBuff(healthBuffAmount, buffDuration);
        }

        // Destroy item after collection
        Destroy(gameObject);
    }
}
