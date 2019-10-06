using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickup : MonoBehaviour
{
    public enum PlayerItemTypes {Eggs, Milk, Water, Flour, Bread, Coins};
    public PlayerItemTypes DropType;

    [Header("Bounce")]
    public float rotationSpeed = 10.0f;
    public float bounceMagnitude = 0.2f;
    public float bounceSpeed = 5.0f;

    private float yPos;

    void Start()
    {
        yPos = this.transform.position.y;
    }

    private void Update()
    {
        this.transform.eulerAngles = new Vector3 (this.transform.eulerAngles.x, this.transform.eulerAngles.y+rotationSpeed*Time.deltaTime, this.transform.eulerAngles.z);
        this.transform.position = new Vector3 (this.transform.position.x, yPos+(Mathf.Sin(Time.time*bounceSpeed)*bounceMagnitude), this.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player ply = other.transform.GetComponent<Player>();

        if (ply)
        {
            
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

            Debug.Log("Picked up " + DropType.ToString());
            Destroy(gameObject);
        }
    }

}
