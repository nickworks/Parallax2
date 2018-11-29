using System.Collections;
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
    public PlayerController player;
    /// <summary>
    /// reference to the player's health and respawn script
    /// </summary>
    public HealthAndRespawn playerHP;
    /// <summary>
    /// reference to the player's jetpack
    /// </summary>
    public Jetpack jetpack;
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
    public void SetPlayer(PlayerController player)
    {
        this.player = player;
        this.playerHP = player.GetComponent<HealthAndRespawn>();
        this.jetpack = player.GetComponent<Jetpack>();
    }
	
	// Update is called once per frame
	void Update () {

        //scale stamina bar
        if(jetpack) fuelBar.rectTransform.localScale = new Vector3(jetpack.fuelPercent, 1, 1);

        //scale health bar
        if(playerHP) healthBar.rectTransform.localScale = new Vector3(playerHP.hpPercent, 1, 1);
        
        //update player's coin number
        Coincounter.text = "Coins: " + Px2.numCoin;
        //update player's key number
        KeyCounter.text = "Keys: " + Px2.numKeys;
    }
}
