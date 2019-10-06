using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickup : MonoBehaviour
{
    public enum PlayerItemTypes {Eggs, Milk, Water, Flour, Bread, Coins};
    public PlayerItemTypes DropType;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 2.0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        Player ply = other.transform.GetComponent<Player>();

        if (ply)
        {
            Debug.Log("Picked up " + transform.name);
            
            switch (DropType)
            {
                case PlayerItemTypes.Eggs:
                    ply.PlayerInv.Eggs++;
                    break;
                case PlayerItemTypes.Milk:
                    ply.PlayerInv.Milk++;
                    break;
                case PlayerItemTypes.Water:
                    ply.PlayerInv.Water++;
                    break;
                case PlayerItemTypes.Flour:
                    ply.PlayerInv.Flour++;
                    break;
                case PlayerItemTypes.Bread:
                    ply.PlayerInv.Bread++;
                    break;
                case PlayerItemTypes.Coins:
                    ply.PlayerInv.Coins++;
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }
    }

}
