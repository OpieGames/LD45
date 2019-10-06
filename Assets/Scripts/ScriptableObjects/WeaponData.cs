using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Weapon  ")]
public class WeaponData : ScriptableObject
{
    [Header("Data")]
    public string NiceName;

    [Range(0.0f, 200.0f)]
    public float Damage;

    [Range(0.0f, 10.0f)]
    public float Range;

    [Header("Visuals")]
    public GameObject Model;
    public Material[] Materials;

}
