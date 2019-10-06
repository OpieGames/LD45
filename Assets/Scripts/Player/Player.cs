using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public struct PlayerInventory
{
    public int Eggs;
    public int Milk;
    public int Water;
    public int Flour;
    public int Bread;
    public int Coins;
}

public class Player : MonoBehaviour
{
    public float DefaultHealth = 100.0f;
    public float MaxHealth = 100.0f;
    public float DefaultFood = 100.0f;
    public float MaxFood = 100.0f;
    public float InteractRadius = 1.25f;
    public WeaponData CurrentWeapon;
    public WeaponData DefaultWeapon;
    public PlayerInventory PlayerInv;

    private float Health;

    private GameObject PlayerModel;

    void Start()
    {
        Health = DefaultHealth;
        PlayerModel = GetComponent<PlayerMove>().model;
        SetWeapon(DefaultWeapon);

        if (CurrentWeapon == null)
        {
            Debug.LogWarning("CurrentWeapon is null!");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(PlayerModel.transform.forward);

            LayerMask lm = LayerMask.GetMask("Living");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, PlayerModel.transform.forward, out hit, CurrentWeapon.Range, lm))
            {
                Debug.Log("HIT: " + hit.transform.name);
                Living living = hit.transform.GetComponent<Living>();
                if (living)
                {
                    Debug.Log("Attacked living: " + living.NiceName);
                    living.TakeDamage(CurrentWeapon.Damage);
                }
            }
        }
        
        if (Input.GetButtonDown("Use"))
        {
            Debug.Log("using");
            Vector3 ourpos = transform.position;
            ourpos.y = 1.0f;
            LayerMask mask = LayerMask.GetMask("Interactable");
            //TODO: make this a trigger on the player so we can highlight interacatables nearby?
            Collider[] hitCols = Physics.OverlapSphere(ourpos, InteractRadius, mask);
            foreach (var col in hitCols)
            {
                Item item = col.GetComponent<Item>();
                if (item)
                {
                    Debug.Log("interacted: " + col.GetComponent<Item>().NiceName);
                    col.GetComponent<Item>().Interact(this);
                }
                else
                {
                    Debug.Log("Found object with Interactable tag but no Item class: " + transform.name);
                }
            }
        }
    }

    public void SetWeapon(WeaponData newWeapon)
    {
        if (newWeapon)
        {
            CurrentWeapon = newWeapon;
        }
        else
        {
            CurrentWeapon =  DefaultWeapon;
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

    private void OnDrawGizmos() {
        Vector3 ourpos = transform.position;
        ourpos.y = 1.0f;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(ourpos, InteractRadius);
    }
}
