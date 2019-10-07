using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthWell : Item
{
    public override void Interact(Player player)
    {
        if (player.PlayerInv.Coins > 0)
        {
            player.PlayerInv.Coins--;
            player.Heal(player.MaxHealth);
        }
    }
}