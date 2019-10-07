using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    public float collisionDamage = 1.0f;
    public GameObject dropPrefab;

    Collider myCollider;
    bool hasDroppedMilk = false;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerComponent = collision.gameObject.GetComponent<Player>();
            playerComponent.TakeDamage(collisionDamage);
        }
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
