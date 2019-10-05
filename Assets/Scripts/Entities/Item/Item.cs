using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : BaseObject
{
    private void Start() {
        gameObject.layer = LayerMask.NameToLayer("Interactable"); // Items are interactable by the player (pickup, etc)
    }
}
