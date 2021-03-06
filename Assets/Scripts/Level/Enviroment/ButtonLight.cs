﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a test script meant to try out the PhysicsButton's Activate and Deactivate messages on a seperate object. 
/// It changes the material to either a set active Material when the button is pressed.
/// When the button is not pressed, it changes the material to the set deactive Material.
/// </summary>
public class ButtonLight : MonoBehaviour {
    /// <summary>
    /// Stores the Renderer of the object we apply this to.
    /// </summary>
    Renderer rend;
    /// <summary>
    /// The material applied to the Renderer when this object is sent an "Activate" message from the button.
    /// </summary>
    public Material litMat;
    /// <summary>
    /// The material applied to the Renderer when this object is sent an "Deactivate" message from the button.
    /// </summary>
    public Material unlitMat;

    /// <summary>
    /// Stores the Renderer of the object this script is applied to as "rend".
    /// Sets the material of the Renderer to the Material stored in "deactiveMat".
    /// </summary>
    void Start () {
        rend = GetComponent<Renderer>();

        if (rend != null)
        {
            rend.material = unlitMat;
        }
	}

    /// <summary>
    /// Is called upon by a PhysicsButton when it has been pressed.
    /// </summary>
    public void ButtonActivate () {
        rend.material = litMat;
    }

    /// <summary>
    /// Is called upon by a PhysicsButton when it is released.
    /// </summary>
    public void ButtonDeactivate ()
    {
        rend.material = unlitMat;
    }

}
