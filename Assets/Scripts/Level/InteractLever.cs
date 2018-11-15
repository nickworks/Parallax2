using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// This class is used to control an InteractLever object. 
/// This object can be used to activate or deactivate a number of objects,
/// when the player interacts with this object.
/// </summary> 
public class InteractLever : MonoBehaviour
{
    /// <summary>
    /// This UnityEvent is called when something is on top of the button.
    /// </summary>
    public UnityEvent onActivate;
    /// <summary>
    /// This UnityEvent is called when the button is clear of anything pushing it down.
    /// </summary>
    public UnityEvent onDeactivate;
    /// <summary>
    /// The Button's activate Material. 
    /// The button applies this material to itself when it is activated.
    /// </summary>
    public Material activeMat;
    /// <summary>
    /// The Button's deactivate Material. 
    /// The button applies this material to itself when it is deactivated.
    /// </summary>
    public Material deactiveMat;
    /// <summary>
    /// Stores the Renderer of the button so that when activated/deactivated, 
    /// it can change materials to reflect beign active/deactive.
    /// </summary>
    Renderer buttonRend;
    /// <summary>
    /// Stores the amount of time that passes (in seconds)
    /// before the button automatically deactivates itself.
    /// Useful for timed puzzles!
    /// </summary>
    [Range(0, 10)]
    public float deactivateTimerMax = 2;
    /// <summary>
    /// Is this meant to be a button on a timer or not?
    /// </summary>
    public bool isTimed;
    /// <summary>
    /// Stores the current time the button has left to cool down.
    /// </summary>
    float deactivateTimerAmount = -1;
    /// <summary>
    /// Stores whether or not the button is cooling down from use.
    /// </summary>
    private bool isButtonActive = false;
    /// <summary>
    /// Stores the Renderer of the Lever in it's active position.
    /// </summary>
    public Renderer activeLever;
    /// <summary>
    /// Stores the Renderer of the Lever in it's deactive position.
    /// </summary>
    public Renderer deactiveLever;

    bool listenForInput = false;

    /// <summary>
    /// Grabs the Renderer for the base of the switch and
    /// sets the switch to its deactive position without using "OnDeactivate.Invoke()".
    /// </summary>
    void Start()
    {
        buttonRend = GetComponentInChildren<Renderer>();
        buttonRend.material = deactiveMat;
        isButtonActive = false;
        activeLever.enabled = false;
        deactiveLever.enabled = true;
    }
    /// <summary>
    /// Update is used to keep track of the cool down time for the switch.
    /// </summary>
    void Update()
    {
        //TODO: prompt the player to press the interact button...

        //if player interacts with the button...

        if (listenForInput && Input.GetButtonDown("Interact"))
        {
            if (isButtonActive)
            {
                Deactivate();
            } else {
                Activate();
                StartCoolDownTimer();
            }
        }
        CoolDownCountdown();
    }
    /// <summary>
    /// Activates when another Collider stays within the trigger area.
    /// </summary>
    /// <param name="other">The collider of the object that is in the trigger area.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") listenForInput = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") listenForInput = false;

    }
    /// <summary>
    /// This is called to set the CoolDownTimer to the same value as CoolDownTimerMax.
    /// </summary>
    private void StartCoolDownTimer()
    {
        deactivateTimerAmount = deactivateTimerMax;
    }
    /// <summary>
    /// This is called every frame to count down coolDownTimer if it is 
    /// set to be higher than 0.
    /// </summary>
    private void CoolDownCountdown()
    {
        if (isTimed && deactivateTimerAmount >= 0)
        {
            deactivateTimerAmount -= Time.deltaTime;
            if (deactivateTimerAmount <= 0 && isButtonActive) Deactivate();
        }

    }
    /// <summary>
    /// This invokes OnActivate and sets the switch to its Activate position.
    /// </summary>
    private void Activate()
    {
        onActivate.Invoke();
        isButtonActive = true;
        activeLever.enabled = true;
        deactiveLever.enabled = false;

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
        activeLever.enabled = false;
        deactiveLever.enabled = true;

        if (deactiveMat != null)
        {
            buttonRend.material = deactiveMat;
        }
    }
}