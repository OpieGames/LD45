﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Item
{
    bool objectiveCompleted = false;

    public override void Interact(Player player)
    {
        if (
        !objectiveCompleted &&
        player.PlayerInv.Water > 0 &&
        player.PlayerInv.Flour >= 4
        )
        {
            player.PlayerInv.Water-=1;
            player.PlayerInv.Flour-=4;
            objectiveCompleted=true;
            GameObject.Instantiate(DropPrefab, transform.position - transform.forward, transform.rotation);
        }
    }
}
