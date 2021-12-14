using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthScript : EnemyHealthScript
{
    //public new void DeductHealth(float deductHealth, PlayerLevelSystem killrLevelSystem) //передаём урон и систему лэвлов
    //{
    //    enemyHealth -= deductHealth;

    //    HealthPercentageChanged?.Invoke(this, enemyHealth / enemyHealthMax);

    //    if (enemyHealth <= 0) //если жизни кончились 
    //    {
    //        killrLevelSystem.GainExperienceFlatRate(ExperienceToGain); //передаём опыт в систему лэвлов
    //        deadEnemy(); //убиваем врага
    //    }
    //}

    //void deadEnemy() //метод смерти врага
    //{
    //    gameObject.SetActive(false);
    //    ResourceManager.ReturnToPool(gameObject);
    //}
}
