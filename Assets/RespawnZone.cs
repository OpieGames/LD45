using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player ply =  other.GetComponent<Player>();
        if (ply)
        {
            ply.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        }
    }
}
