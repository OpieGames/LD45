using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowPen : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cow"))
        {
            Debug.Log("Cow entered pen trigger");
            Cow cowComponent = other.gameObject.GetComponent<Cow>();
            cowComponent.DropItem();
        }
    }
}
