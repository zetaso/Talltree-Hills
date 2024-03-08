using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int max_health;
    public int health { get; private set; }

    public UnityAction onDie;

    void Start()
    {
        health = max_health;
    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);

        if (health == 0)
            onDie.Invoke();
    }
}
