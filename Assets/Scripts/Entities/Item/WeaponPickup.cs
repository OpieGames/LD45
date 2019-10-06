using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Item
{
    public WeaponData weaponData;
    public GameObject WeaponMeshHolder;

    private Vector3 posOffset;

    private void Start()
    {
        Instantiate(weaponData.Model, WeaponMeshHolder.transform);
        posOffset = WeaponMeshHolder.transform.position;
    }

    private void LateUpdate() {
        Vector3 tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * 1.2f) * 0.1f;
 
        WeaponMeshHolder.transform.position = tempPos;
    }

    public override void Interact(Player player)
    {
        player.SetWeapon(weaponData);
        Destroy(gameObject);
    }
}
