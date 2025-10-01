using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image _healthbarsprite;
    public void UpdateHealthbar(float maxhealth, float currenthealth)
    {
        _healthbarsprite.fillAmount = currenthealth / maxhealth;
    }
}
