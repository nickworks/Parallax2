using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script is used to control an InteractButton object. 
/// These buttons are activated when the player is within range and they press the "Interact" button.
/// </summary>
public class InteractButton : MonoBehaviour {
    /// <summary>
    /// This UnityEvent is meant to call itself when the button is activated by the player.
    /// </summary>
    public UnityEvent onActivate;
    /// <summary>
    /// This UnityEvent is meant to call itself when the button is deactivated by the player, or when a set amount of time has passed.
    /// </summary>
    public UnityEvent onDeactivate;
    /// <summary>
    /// This stores the Button's activate Material. The button applies this material to itself when it is activated.
    /// </summary>
    public Material activeMat;
    /// <summary>
    /// This stores the Button's deactivate Material. The button applies this material to itself when it is deactivated.
    /// </summary>
    public Material deactiveMat;
    /// <summary>
    /// This stores the Renderer of the button so that when activated/deactivated, 
    /// it can change materials to reflect the button being activated/deactivated.
    /// </summary>
    Renderer buttonRend;
    /// <summary>
    /// This stores the amount of time that passes (in seconds) before the button can be used again.
    /// </summary>
    [Range(0, 10)]
    public float coolDownTimerMax = 2;
    /// <summary>
    /// This stores the current time the button has left to cool down.
    /// </summary>
    float coolDownTimer;
    /// <summary>
    /// This stores whether the button is meant to be timed or not.
    /// </summary>
    public bool isTimed;
    /// <summary>
    /// This stores whether or not the button is cooling down from use.
    /// </summary>
    private bool isButtonActive = false;
    /// <summary>
    /// This stores whether or not the player is colliding with the object.
    /// </summary>
    private bool isPlayerColliding = false;

    /// <summary>
    /// Grabs the Renderer for the base of the switch and
    /// sets the switch to its deactive position without using "OnDeactivate.Invoke()".
    /// </summary>
    void Start () {
        buttonRend = GetComponentInChildren<Renderer>();
        buttonRend.material = deactiveMat;
	}
    /// <summary>
    /// Controls the button's state to properly activate/deactivate it.
    /// </summary>
    void Update ()
    {
        if (isPlayerColliding)
        {
            //TODO: prompt the player to press the interact button...

            //if player interacts with the button...
            float interact = Input.GetAxis("Submit");

            if (interact > 0 && coolDownTimer < 0)
            {
                switch (isButtonActive)
                {
                    case true:
                        Deactivate();
                        SetCoolDownTimer();
                        break;

                    case false:
                        Activate();
                        SetCoolDownTimer();
                        break;
                }
            }
        }
        CoolDownCountdown();
    }
    /// <summary>
    /// This activates when an object enters this object's trigger area.
    /// It checks to see if the object that entered is the player.
    /// </summary>
    /// <param name="other">The collider of the object that has entered the area.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.isTrigger) isPlayerColliding = true;
    }
    /// <summary>
    /// This activates when an object exits this object's trigger area.
    /// It checks to see if the object that exited is the player.
    /// </summary>
    /// <param name="other">The collider of the object that has exited the area.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.isTrigger) isPlayerColliding = false;
    }
    /// <summary>
    /// This is called to set the cooldownTimer to it's max value.
    /// </summary>
    private void SetCoolDownTimer()
    {
        coolDownTimer = coolDownTimerMax;
    }
    /// <summary>
    /// This is called every frame to make sure the cooldown for the button counts down.
    /// </summary>
    private void CoolDownCountdown()
    {
        if (coolDownTimer >= 0)
        {
            coolDownTimer -= Time.deltaTime;
            //print("Cooling down: " + coolDownTimer + " seconds remaining.");

            if (isTimed)
            {
                if (coolDownTimer < 0 && isButtonActive) Deactivate();
            }
        }

    }
    /// <summary>
    /// This invokes OnActivate and sets the switch to its Activate position.
    /// </summary>
    private void Activate()
    {
        onActivate.Invoke();
        isButtonActive = true;

        if (activeMat != null)
        {
            buttonRend.material = activeMat;
        }
    }
    /// <summary>
    /// This invokes OnDeactivate and sets the switch to its Deactivate position.
    /// </summary>
    private void Deactivate()
    {
        onDeactivate.Invoke();
        isButtonActive = false;

        if (deactiveMat != null)
        {
            buttonRend.material = deactiveMat;
        }
    }
}
