using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class makes something move back and forth. Intended as a tool for debugging & playtesting.
/// </summary>
public class MotionTest : MonoBehaviour {

    /// <summary>
    /// The world position this object oscillates around.
    /// </summary>
    public Vector3 home { get; private set; }
    /// <summary>
    /// How fast to oscillate
    /// </summary>
    [Range(0,20)] public float period = 10;
    /// <summary>
    /// How far to oscillate
    /// </summary>
    [Range(0,10)] public float amplitude = 1;
    /// <summary>
    /// Whether or not to wiggle on the x axis
    /// </summary>
    public bool wiggleX = true;
    /// <summary>
    /// Whether or not to wiggle on the y axis
    /// </summary>
    public bool wiggleY = true;
    /// <summary>
    /// Whether or not to wiggle on the z axis
    /// </summary>
    public bool wiggleZ = true;
    
    /// <summary>
    /// Sets home
    /// </summary>
    void Start()
    {
        home = transform.position;
    }
    /// <summary>
    /// Tick
    /// </summary>
	void Update () {

        float val = Mathf.Sin(Time.time * period) * amplitude;

        Vector3 offset = new Vector3();
        if (wiggleX) offset.x = val;
        if (wiggleY) offset.y = val;
        if (wiggleZ) offset.z = val;

        transform.position = home + offset * amplitude;
	}
}
