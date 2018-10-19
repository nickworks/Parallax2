using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class makes something move back and forth. Intended as a tool for debugging & playtesting.
/// </summary>
public class MotionTest : MonoBehaviour {

    public Vector3 home { get; private set; }
    [Range(0,20)] public float period = 10;
    [Range(0,3)] public float amplitude = 1;

    public bool wiggleX = true;
    public bool wiggleY = true;
    public bool wiggleZ = true;
    
    /// <summary>
    /// Set home
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
