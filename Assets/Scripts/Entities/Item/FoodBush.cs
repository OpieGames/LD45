using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBush : Item
{
   [Header("Food")]
   public float FoodRestored = 25.0f;

   public override void Interact(Player player)
   {
       Destroy(gameObject);
   }
}
