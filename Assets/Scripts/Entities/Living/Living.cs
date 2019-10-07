using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : BaseObject
{
    [Header("Living")]
    public float StartingHealth;
    private float Health;

    [Header("Sounds")]
    public AudioSource AudioSrc;
    public AudioClip IdleSound;
    public AudioClip DamageSound;
    public AudioClip DieSound;
    public float IdleSoundTimer = 10.0f;
    private float idleSoundCurrent = 0.0f;

    private void Start()
    {
        Health = StartingHealth;
        AudioSrc =  GetComponent<AudioSource>();
        idleSoundCurrent = UnityEngine.Random.Range(0.0f, 5.0f);
        IdleSoundTimer += idleSoundCurrent;
    }

    private void Update()
    {
        if (idleSoundCurrent >= IdleSoundTimer)
        {
            AudioSrc.volume = UnityEngine.Random.Range(0.4f, 0.5f);
            AudioSrc.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            AudioSrc.PlayOneShot(IdleSound);
            idleSoundCurrent = 0.0f;
        }
        
        idleSoundCurrent += Time.deltaTime;
    }

    public float GetHealth()
    {
        return Health;
    }

    public void TakeDamage(float damage)
    {
        float resulthp = Health - damage;
        if (resulthp <= 0.0f)
        {
            Health = 0.0f;
            Debug.Log(transform.name + " dead!");
            AudioSrc.Stop();
            AudioSrc.volume = UnityEngine.Random.Range(0.8f, 0.9f);
            AudioSrc.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            AudioSrc.PlayOneShot(DamageSound);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(transform.name + " damaged for: " + damage);
            Health = resulthp;
            AudioSrc.volume = UnityEngine.Random.Range(0.8f, 0.9f);
            AudioSrc.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            AudioSrc.PlayOneShot(DieSound);
        }
    }


}
