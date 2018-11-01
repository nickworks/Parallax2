using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// holds references to objects within the UI canvas, affects them according to variables within the player (pawn)
/// </summary>
public class HUDController : MonoBehaviour {
    public PlayerController pawn;
    /// <summary>
    ///  fuel bar UI element
    /// </summary>
    public Image fuelBar;
    /// <summary>
    ///  health bar UI element
    /// </summary>
    public Image healthBar;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (pawn == null) return;
       pawn.fuelPercent = new Vector3(pawn.jetpackFuel / 100, 1, 1);
       fuelBar.rectTransform.localScale = pawn.fuelPercent;
       pawn.jetpackFuel = Mathf.Clamp(pawn.jetpackFuel, 0, 100);
    }
}
