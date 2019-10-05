using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : BaseObject
{
    public float StartingHealth;
    private float Health;

    private void Start() {
        Health = StartingHealth;
    }

    public float GetHealth()
    {
        return Health;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(transform.name + " damaged for: " + damage);
        float resulthp = Health - damage;
        if (resulthp <= 0.0f)
        {
            Health = 0.0f;
            Debug.Log(transform.name + " dead!");
            Destroy(gameObject);
        }
        else
        {
            Health = resulthp;
        }
    }
}
