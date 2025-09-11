using UnityEngine;

public class DefBuff : MonoBehaviour
{
    public float collectDistance = 2f; // how close player must be
    public float defenseBuffAmount = 5f;
    public float buffDuration = 10f;   // seconds

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
        Debug.Log("✅ Player collected Defense Buff: " + gameObject.name);

        // Apply +5 defense buff
        PlayerBuffs buffs = player.GetComponent<PlayerBuffs>();
        if (buffs != null)
        {
            buffs.ApplyDefenseBuff(defenseBuffAmount, buffDuration);
        }

        // Destroy item after collection
        Destroy(gameObject);
    }
}
