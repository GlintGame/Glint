using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthbar : MonoBehaviour
{
    public GameObject HealthbarMask;
    public GameObject HealthbarFilled;
    
    public void UpdateHealthbar(int currentHealth, int maxHealth)
    { 
        float proportion = currentHealth > 0 ? (float)currentHealth / (float)maxHealth : 1;
        this.HealthbarMask.transform.localScale = new Vector3(1, proportion, 1);
        this.HealthbarFilled.transform.localScale = new Vector3(1, 1 / proportion, 1);
    }
}
