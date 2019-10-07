using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{

    public GameObject UIReference;
    [TextArea]
    public string prompt;
    // Start is called before the first frame update
    void Start()
    {
        if (!UIReference)
        {
            UIReference = GameObject.Find("UI");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (UIReference)
            {
                UIReference.GetComponent<PlayerHUD>().DisplaySignPrompt(prompt);
            }
            else
            {
                Debug.Log("Sign entity could not find UI GameObject!");
            }
        }
    }
}
