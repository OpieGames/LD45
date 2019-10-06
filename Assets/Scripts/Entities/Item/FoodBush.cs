using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBush : Item
{
   public float FoodRestored = 25.0f;

   public override void Interact(Player player)
   {
       player.EatFood(FoodRestored);
       Destroy(gameObject);
   }
}
