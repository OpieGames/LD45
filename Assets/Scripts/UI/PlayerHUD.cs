using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI HealthUI;
    public TextMeshProUGUI FoodUI;

    private Player PlayerRef;
    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnGUI()
    {
        HealthUI.text = PlayerRef.GetHealth().ToString();
        FoodUI.text = PlayerRef.GetFood().ToString();
    }
}
