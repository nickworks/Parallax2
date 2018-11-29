using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script is meant to be used on a Trigger to signal to an object when the Trigger is activated or deactivated.
/// </summary>
public class PhysicsButton : MonoBehaviour {

    /// <summary>
    /// Stores a reference to the object the button is attached to.
    /// </summary>
    //public GameObject target;

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
    /// Stores whether or not the player is colliding with the object.
    /// </summary>
    private bool isPlayerColliding = false;

    /// <summary>
    /// Used to store the Button's renderer after it's been instantiated.
    /// </summary>
    private void Start()
    {
        buttonRend = GetComponentInChildren<Renderer>();
        
    }

    private void Update()
    {
        if(isPlayerColliding)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }
    /// <summary>
    /// Activates when another Collider enters the trigger area.
    /// </summary>
    /// <param name="other">The collider of the object that has entered the trigger area.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.isTrigger) isPlayerColliding = true;
    }

    /// <summary>
    /// Activates when another Collider exits the trigger area.
    /// </summary>
    /// <param name="other">The collider of the object that has exited the trigger area.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.isTrigger) isPlayerColliding = false;
    }
    /// <summary>
    /// This is called when the button is activated. It invokes the object's onActivate function.
    /// </summary>
    private void Activate()
    {
        onActivate.Invoke();

        if (activeMat != null)
        {
            buttonRend.material = activeMat;
        }
    }
    /// <summary>
    /// This is called when the button is deactivated. It invokes the object's onDeactivate function.
    /// </summary>
    private void Deactivate()
    {
        onDeactivate.Invoke();

        if (deactiveMat != null)
        {
            buttonRend.material = deactiveMat;
        }
    }
}
