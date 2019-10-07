using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    public float collisionDamage = 1.0f;
    public GameObject dropPrefab;

    Collider myCollider;
    bool hasDroppedMilk = false;

    [Header("Sounds")]
    public AudioSource AudioSrc;
    public AudioClip IdleSound;
    public float IdleSoundTimer = 12.0f;
    private float idleSoundCurrent = 0.0f;

    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        AudioSrc =  GetComponent<AudioSource>();
        idleSoundCurrent = UnityEngine.Random.Range(0.0f, 5.0f);
        IdleSoundTimer += UnityEngine.Random.Range(4.0f, 8.0f);;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerComponent = collision.gameObject.GetComponent<Player>();
            playerComponent.TakeDamage(collisionDamage);

        }
    }

    void Update()
    {
        if (idleSoundCurrent >= IdleSoundTimer)
        {
            AudioSrc.volume = UnityEngine.Random.Range(0.4f, 0.5f);
            AudioSrc.pitch = UnityEngine.Random.Range(0.96f, 1.04f);
            AudioSrc.PlayOneShot(IdleSound);
            idleSoundCurrent = 0.0f;
        }
        
        idleSoundCurrent += Time.deltaTime;
    }

    // Drop the milk prefab. This is called by the pen trigger volume when a cow enters it.
    public void DropItem()
    {
        if (!hasDroppedMilk)
        {
            Instantiate(dropPrefab, transform.position - transform.forward * 2.0f, transform.rotation);
            hasDroppedMilk = true;
        }
    }
}
