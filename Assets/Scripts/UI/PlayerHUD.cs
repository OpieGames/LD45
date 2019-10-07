using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI HealthUI;
    public TextMeshProUGUI PromptUI;

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
        ClearSignPrompt();
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
}
