﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// holds references to objects within the UI canvas, affects them according to variables within the player (pawn)
/// </summary>
public class HUDController : MonoBehaviour {
    /// <summary>
    /// Reference to the player's PlayerController 
    /// </summary>
    public PlayerController pawn;
    /// <summary>
    /// reference to the player's health and respawn script
    /// </summary>
    public HealthAndRespawn playerHP;
    /// <summary>
    ///  fuel bar UI element
    /// </summary>
    public Image fuelBar;
    /// <summary>
    ///  health bar UI element
    /// </summary>
    public Image healthBar;
    /// <summary>
    /// Text element of UI, displaying player's current Coins
    /// </summary>
    public Text Coincounter;
    /// <summary>
    /// Text element of UI, displaying player's current keys
    /// </summary>
    public Text KeyCounter;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (pawn == null) return;
        
        if (playerHP == null) return;
        
        //scale stamina bar
       pawn.fuelPercent = new Vector3(pawn.jetpackFuel / 100, 1, 1);
       fuelBar.rectTransform.localScale = pawn.fuelPercent;
       pawn.jetpackFuel = Mathf.Clamp(pawn.jetpackFuel, 0, 100);
        //scale health bar
       playerHP.hpPercent = new Vector3(playerHP.hpCurrent / playerHP.hpMax, 1, 1);
       healthBar.rectTransform.localScale = playerHP.hpPercent;
        //update player's coin number
        Coincounter.text = "Coins: " + Px2.numCoin;
        //update player's key number
        KeyCounter.text = "Keys: " + Px2.numKeys;

        
    }
}
