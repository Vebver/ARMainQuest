using UnityEngine;

public class AttackBuffItem : MonoBehaviour
{
    public float collectDistance = 2f;
    public float attackBuffAmount = 5f;
    public float buffDuration = 10f; // 10 seconds duration

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        if (Vector3.Distance(transform.position, player.position) <= collectDistance)
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        Debug.Log("🟢 Player collected Attack Buff: " + gameObject.name);

        PlayerBuffs buffs = player.GetComponent<PlayerBuffs>();
        if (buffs != null)
        {
            buffs.ApplyAttackBuff(attackBuffAmount, buffDuration);
        }

        Destroy(gameObject);
    }
}
