using UnityEngine;

public class AttackBuff : MonoBehaviour
{
    public float attackIncrease = 10f;
    public float duration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBuffs buffs = other.GetComponent<PlayerBuffs>();
            if (buffs != null)
            {
                buffs.ApplyAttackBuff(attackIncrease, duration);
            }
            Destroy(gameObject);
        }
    }
}