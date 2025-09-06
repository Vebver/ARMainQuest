using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuff : MonoBehaviour
{
    public float DefenseIncrease = 10f;
    public float duration = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBuffs buffs = other.GetComponent<PlayerBuffs>();
            if (buffs != null)
            {
                buffs.ApplyAttackBuff(DefenseIncrease, duration);
            }
            Destroy(gameObject);
        }
    }
}
