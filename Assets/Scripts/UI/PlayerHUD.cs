using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI HealthUI;
    public TextMeshProUGUI PromptUI;

    [Header("Player Inventory Text")]
    public GameObject EggsGroup;
    public TextMeshProUGUI EggsValue;
    public GameObject MilkGroup;
    public TextMeshProUGUI MilkValue;
    public GameObject WaterGroup;
    public TextMeshProUGUI WaterValue;
    public GameObject FlourGroup;
    public TextMeshProUGUI FlourValue;
    public GameObject BreadGroup;
    public TextMeshProUGUI BreadValue;
    public GameObject CoinsGroup;
    public TextMeshProUGUI CoinsValue;

    private Player PlayerRef;
    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ClearSignPrompt();
        ClearInventoryList();
    }

    private void OnGUI()
    {
        HealthUI.text = PlayerRef.GetHealth().ToString();
        PlayerInventory inv = PlayerRef.PlayerInv;

        if (inv.Eggs > 0)
            EggsGroup.SetActive(true);
        if (inv.Milk > 0)
            MilkGroup.SetActive(true);
        if (inv.Water > 0)
            WaterGroup.SetActive(true);
        if (inv.Flour > 0)
            FlourGroup.SetActive(true);
        if (inv.Bread > 0)
            BreadGroup.SetActive(true);
        if (inv.Coins > 0)
            CoinsGroup.SetActive(true);

        EggsValue.text = inv.Eggs.ToString();
        MilkValue.text = inv.Milk.ToString();
        WaterValue.text = inv.Water.ToString();
        FlourValue.text = inv.Flour.ToString();
        BreadValue.text = inv.Bread.ToString();
        CoinsValue.text = inv.Coins.ToString();
    }

    public void DisplaySignPrompt(string prompt)
    {
        PromptUI.text = prompt;
        // Hacky way to display a prompt for a reasonable amount of read time
        // Assume read speed of 120 words/min, i.e. 0.5s per word.
        // Assume average word length of 5 characters.
        float promptLingerSeconds = 0.5f * prompt.Length / 5.0f;
        Invoke("ClearSignPrompt", promptLingerSeconds);
    }

    public void ClearSignPrompt()
    {
        PromptUI.text = "";
    }

    public void ClearInventoryList()
    {
        EggsGroup.SetActive(false);
        MilkGroup.SetActive(false);
        WaterGroup.SetActive(false);
        FlourGroup.SetActive(false);
        BreadGroup.SetActive(false);
        CoinsGroup.SetActive(false);
    }
}
