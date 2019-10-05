using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float DefaultHealth;
    public float MaxHealth;
    private float Health;

    void Start()
    {
        Health = DefaultHealth;
    }

    void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            Debug.Log("using");
            Vector3 ourpos = transform.position;
            ourpos.y = 1.0f;
            LayerMask mask = LayerMask.GetMask("Interactable");
            Collider[] hitCols = Physics.OverlapSphere(ourpos, 1.2f, mask);
            foreach (var col in hitCols)
            {
                Debug.Log(col.name);
            }
        }
    }

    public float GetHealth()
    {
        return Health;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player damaged for: " + damage);
        float resulthp = Health - damage;
        if (resulthp <= 0.0f)
        {
            Health = 0.0f;
            Debug.Log("Player dead!");
        }
        else
        {
            Health = resulthp;
        }
    }

    public void Heal(float amount)
    {
        Debug.Log("Player healed for: " + amount);
        float resulthp = Health + amount;
        if (resulthp >= MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health = resulthp;
        }
    }
}
