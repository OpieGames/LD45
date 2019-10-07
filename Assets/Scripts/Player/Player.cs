using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
    public float attackRange = 1.4f;
    public float attackCooldown = 0.9f;
    public WeaponData CurrentWeapon;
    public WeaponData DefaultWeapon;
    public PlayerInventory PlayerInv;

    private float Health;
    private float currentAttackCooldown = 0;

    private GameObject PlayerModel;
    private Animator animator;

    [Header("Sounds")]
    public AudioSource PickupSrc;
    public AudioSource PunchSrc;
    public AudioClip[] HurtSounds;
    public AudioSource HurtSrc;

    void Start()
    {
        Health = DefaultHealth;
        PlayerModel = GetComponent<PlayerMove>().model;
        animator = GetComponent<Animator>();
        SetWeapon(DefaultWeapon);

        if (CurrentWeapon == null)
        {
            Debug.LogWarning("CurrentWeapon is null!");
        }
    }

    void Update()
    {
        if (currentAttackCooldown > 0.0f) {
            currentAttackCooldown -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            // Debug.Log(PlayerModel.transform.forward);
            if (currentAttackCooldown <= 0.0f)
            {
                currentAttackCooldown = attackCooldown;
                animator.SetTrigger("attack");
                PunchSrc.volume = UnityEngine.Random.Range(0.7f, 0.8f);
                PunchSrc.pitch = UnityEngine.Random.Range(0.98f, 1.02f);
                PunchSrc.PlayDelayed(0.1f);
                
            }
        }

        if (Input.GetButtonDown("Use"))
        {
            Debug.Log("using");
            Vector3 ourpos = transform.position;
            ourpos.y += 1.0f;
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
                    return;
                }
                else
                {
                    Debug.Log("Found object with Interactable tag but no Item class: " + transform.name);
                }
            }
        }

        if (Input.GetButtonDown("Respawn"))
        {
            transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        }
    }

    public void PickedupItem()
    {
        PickupSrc.volume = 0.8f;
        PickupSrc.Play();
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

        int index = UnityEngine.Random.Range(0, HurtSounds.Length);
        HurtSrc.volume = UnityEngine.Random.Range(0.7f, 0.85f);
        HurtSrc.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        HurtSrc.PlayOneShot(HurtSounds[index]);

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

    public void DoAttack() {
        LayerMask lm = LayerMask.GetMask("Living");
        RaycastHit hit;
        Vector3 boxHalfExtents = new Vector3(
            attackRange/2.0f,
            attackRange/2.0f,
            attackRange/2.0f
        );
        if (Physics.SphereCast(transform.position, 0.5f * attackRange, PlayerModel.transform.forward, out hit, attackRange, lm, QueryTriggerInteraction.UseGlobal))
        // if (Physics.CheckBox(transform.position,boxHalfExtents,PlayerModel.transform.rotation,lm))
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
}
