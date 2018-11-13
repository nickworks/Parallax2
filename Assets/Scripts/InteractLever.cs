using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// This class is used to control an InteractLever object. 
/// This object can be used to activate or deactivate a number of objects,
/// when the player interacts with this object.
/// </summary>


//TODO: Make two seperate levers for the on/off position, and hide/unhide when swapping. 
public class InteractLever : MonoBehaviour {
    /// <summary>
    /// This UnityEvent is called when something is on top of the button.
    /// </summary>
    public UnityEvent onActivate;
    /// <summary>
    /// This UnityEvent is called when the button is clear of anything pushing it down.
    /// </summary>
    public UnityEvent onDeactivate;
    /// <summary>
    /// The Button's activate Material. The button applies this material to itself when it is activated.
    /// </summary>
    public Material activeMat;
    /// <summary>
    /// The Button's deactivate Material. The button applies this material to itself when it is deactivated.
    /// </summary>
    public Material deactiveMat;
    /// <summary>
    /// Stores the Renderer of the button so that when activated/deactivated, it can change materials to reflect beign active/deactive.
    /// </summary>
    Renderer buttonRend;
    /// <summary>
    /// Stores the amount of time that passes (in seconds) before the button can be used again.
    /// </summary>
    public float coolDownTimerMax = 2;
    /// <summary>
    /// Stores the current time the button has left to cool down.
    /// </summary>
    float coolDownTimer;
    /// <summary>
    /// Stores whether or not the button is cooling down from use.
    /// </summary>
    private bool isButtonCoolingDown = false;
    // Use this for initialization
    void Start() {
        buttonRend = GetComponentInChildren<Renderer>();
        buttonRend.material = deactiveMat;
    }
	// Update is called once per frame
	void Update () {
        if (isButtonCoolingDown)
        {
            coolDownTimer -= Time.deltaTime;
            print("Cooling down: " + coolDownTimer + " seconds remaining.");

            if (coolDownTimer < 0)
            {
                onDeactivate.Invoke();
                isButtonCoolingDown = false;
                if (deactiveMat != null)
                {
                    buttonRend.material = deactiveMat;
                }
            }

        }
    }
    /// <summary>
    /// Activates when another Collider stays within the trigger area.
    /// </summary>
    /// <param name="other">The collider of the object that is in the trigger area.</param>
    private void OnTriggerStay(Collider other)
    {
        //TODO: prompt the player to press the interact button...

        //if player interacts with the button...
        float interact = Input.GetAxis("Submit");

        if (interact > 0 && !isButtonCoolingDown)
        {
            onActivate.Invoke();
            isButtonCoolingDown = true;
            SetCoolDownTimer();

            if (activeMat != null)
            {
                buttonRend.material = activeMat;
            }
        }
    }
    private void SetCoolDownTimer()
    {
        coolDownTimer = coolDownTimerMax;
    }
}
