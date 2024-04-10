using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image healthBarFill;

    // Update is called once per frame
    void Update()
    {
        if(playerHealth != null && healthBarFill != null)
        {
            float fillNum = (float)playerHealth.GetCurrentHealth() / playerHealth.maxHealth;
            
            healthBarFill.fillAmount = fillNum;

            Debug.Log("Health Bar Fill Amount: " + fillNum);
        }
    }
}
