using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public EnemyRanged shooter;
    public float speed;
    public float maxLifetime = 10.0f;

    private int damage;

    // Use this for initialization
    void Start()
    {
        damage = shooter.attackDamage;
        Destroy(gameObject, maxLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerComponent = other.GetComponent<Player>();
            playerComponent.TakeDamage(damage);
            shooter.RegisterHit();
        }
        if (other.gameObject.layer != LayerMask.GetMask("Living"))
        {
            Destroy(gameObject);
        }
    }
}
