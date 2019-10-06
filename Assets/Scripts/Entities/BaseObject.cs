using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public string NiceName;

    [Header("Drops")]
    public GameObject DropPrefab;
    [Range(0, 10)]
    public int DropAmount;

}
