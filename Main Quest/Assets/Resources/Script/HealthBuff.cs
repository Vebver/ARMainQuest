using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
    public float HealthIncrease = 20f;
    public float duration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBuffs buffs = other.GetComponent<PlayerBuffs>();
            if (buffs != null)
            {
                buffs.ApplyAttackBuff(HealthIncrease, duration);
            }
            Destroy(gameObject);
        }
    }
}
