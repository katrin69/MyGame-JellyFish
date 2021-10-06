using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //’п врага
    public int health;
    public int healthMax;

    void Start()
    {
        //’п становитс€ максимальным при старте
        health = healthMax;
    }
}
