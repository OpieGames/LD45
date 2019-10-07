using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : Item
{
    bool objectiveCompleted = false;
    public override void Interact(Player player)
    {
        if (!objectiveCompleted)
        {
            GameObject.Instantiate(DropPrefab,
                transform.position + transform.forward,
                transform.rotation);
            objectiveCompleted = true;
        }
    }
}
