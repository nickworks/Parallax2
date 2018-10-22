﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is a pseudo-singleton. 
/// </summary>
[RequireComponent(typeof(DollyZoom))]
[RequireComponent(typeof(EaseToTarget))]
public class CameraController : MonoBehaviour {

    /// <summary>
    /// The singleton.
    /// </summary>
    public static CameraController main { get; private set; }

    /// <summary>
    /// The dolly behavior. Also controls zoom.
    /// </summary>
    public DollyZoom dolly { get; private set; }
    /// <summary>
    /// The chase camera behavior. Give it a target, and it follows it!
    /// </summary>
    public EaseToTarget chase { get; private set; }

	// Use this for initialization
	void Start () {
        if (main == null) // first one spawning
        {
            dolly = GetComponent<DollyZoom>();
            chase = GetComponent<EaseToTarget>();
            main = this;
            
        } else
        {
            Destroy(gameObject); // there can be only one
        }
	}
}