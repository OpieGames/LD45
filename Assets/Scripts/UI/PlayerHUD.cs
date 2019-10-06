using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI HealthUI;

    [Header("Player Inventory Text")]
    public TextMeshProUGUI EggsValue;
    public TextMeshProUGUI MilkValue;
    public TextMeshProUGUI WaterValue;
    public TextMeshProUGUI FlourValue;
    public TextMeshProUGUI BreadValue;
    public TextMeshProUGUI CoinsValue;

    private Player PlayerRef;
    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnGUI()
    {
        HealthUI.text = PlayerRef.GetHealth().ToString();

        EggsValue.text = PlayerRef.PlayerInv.Eggs.ToString();
        MilkValue.text = PlayerRef.PlayerInv.Milk.ToString();
        WaterValue.text = PlayerRef.PlayerInv.Water.ToString();
        FlourValue.text = PlayerRef.PlayerInv.Flour.ToString();
        BreadValue.text = PlayerRef.PlayerInv.Bread.ToString();
        CoinsValue.text = PlayerRef.PlayerInv.Coins.ToString();
    }
}
