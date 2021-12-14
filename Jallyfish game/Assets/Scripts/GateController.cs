using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public event Action<EScene> LoadSceneRequest;

    public Transform Gate;
    public Vector3 GateMovement = new Vector3(0, -0.1f, 0);

    public EScene NextScene;

    private EnemyHealthScript BossHealth;

    public bool Triggered;

    public float Timer;
    public float MaxTime = 5;

    public void SetTargetHealthScript(EnemyHealthScript enemyHealthScript)
    {
        BossHealth = enemyHealthScript;
        BossHealth.HealthPercentageChanged += EnemyHealthScript_HealthPercentageChanged;
    }

    private void EnemyHealthScript_HealthPercentageChanged(EnemyHealthScript arg1, float arg2)
    {
        if (arg2 <= 0)
        {
            Triggered = true;
        }
    }

    public void Update()
    {
        if (Triggered)
        {
            Timer += Time.deltaTime;

            if (Timer >= MaxTime)
            {
                Timer = 0;
                Triggered = false;
            }
            else
            {
                Gate.position += GateMovement;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (LoadSceneRequest != null)
            {
                LoadSceneRequest(NextScene);
            }
        }
    }

    private void OnDestroy()
    {
        BossHealth.HealthPercentageChanged -= EnemyHealthScript_HealthPercentageChanged;
    }
}
