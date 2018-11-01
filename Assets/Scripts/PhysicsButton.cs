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
    /// Used to store the Button's renderer after it's been instantiated.
    /// </summary>
    private void Start()
    {
        buttonRend = GetComponentInChildren<Renderer>();
        
    }
    /// <summary>
    /// Activates when another Collider enters the trigger area. This sends an "Activate" message to its Target.
    /// </summary>
    /// <param name="other">The collider of the object that has entered the trigger area.</param>
    private void OnTriggerEnter(Collider other)
    {
        print("hi");
        onActivate.Invoke();
       
        if (activeMat != null)
        {
            buttonRend.material = activeMat;
        }
    }

    /// <summary>
    /// Activates when another Collider exits the trigger area. This sends a "Deactivate" message to its Target.
    /// </summary>
    /// <param name="other">The collider of the object that has exited the trigger area.</param>
    private void OnTriggerExit(Collider other)
    {
        onDeactivate.Invoke();
        
        if(deactiveMat != null)
        {
            buttonRend.material = deactiveMat;
        }
    }
}
