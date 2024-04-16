using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private Image bossHealthBarFill;
    
    // Update is called once per frame
    void Update()
    {
        if(bossHealth != null && bossHealthBarFill != null)
        {
            float fillNum = (float)bossHealth.GetCurrentBossHealth() / bossHealth.maxHealth;

            bossHealthBarFill.fillAmount = fillNum;
        }
    }
}
