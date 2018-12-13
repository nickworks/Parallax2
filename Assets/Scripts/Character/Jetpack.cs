using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Jetpack : MonoBehaviour {

    /// <summary>
    /// Tracking if the player is able to use the jetpack in this scene or not;
    /// </summary>
    public bool isJetpackEnabled = true;
    /// <summary>
    /// the velocity applied to the player each frame they are using the jetpack.
    /// </summary>
    private float jetpackAcceleration = 150;
    /// <summary>
    /// How should thrust falloff?
    /// </summary>
    public AnimationCurve thrustFalloff;
    public float maximumVelocity = 10;
    /// <summary>
    /// How much fuel we currently have, measured in seconds.
    /// </summary>
    public float jetpackFuelAmount { private set; get; }
    /// <summary>
    /// How much fuel can we store, measured in seconds.
    /// </summary>
    public float jetpackFuelMax = 3;
    /// <summary>
    /// tracking how much current fuel the player has versus maximum fuel. Used to scale the UI fuel bar
    /// </summary>
    public float fuelPercent
    {
        get
        {
            return jetpackFuelAmount / jetpackFuelMax;
        }
    }
    /// <summary>
    /// a reference to the Parent of the flame effect attached to the player object.
    /// </summary>
    public GameObject jetpackFumes;

    private PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
        if (isJetpackEnabled) jetpackFuelAmount = jetpackFuelMax;
    }
    void Update()
    {
        if (jetpackFuelAmount <= 0) jetpackFumes.SetActive(false);
        if (player.isGrounded)
        {
            jetpackFuelAmount = jetpackFuelMax;
            jetpackFumes.SetActive(false);
        }

        if (Input.GetButton("Jump"))
        {
            if (player.airJumpsCount == 0 && jetpackFuelAmount > 0) //no jumps left, begin consuming fuel
            {
                jetpackFumes.SetActive(true);
                jetpackFuelAmount -= Time.deltaTime;
                float thrust = GetThrustAmount();
                player.Impulse(new Vector3(0, thrust, 0), true);
            }
        } else { 
            jetpackFumes.SetActive(false);
        }
        if (jetpackFuelAmount <= 0) jetpackFumes.SetActive(false);
    }
    float GetThrustAmount()
    {
        float percent = player.GetVelocity().y / maximumVelocity;
        percent = Mathf.Clamp(percent, 0, 1);
        return thrustFalloff.Evaluate(percent) * jetpackAcceleration;
    }
}
