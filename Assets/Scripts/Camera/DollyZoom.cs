using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This behavior eases the camera towards a target fov and distance.
/// </summary>
public class DollyZoom : MonoBehaviour {
    /// <summary>
    /// The camera to dolly/zoom
    /// </summary>
    public Camera cam;
    /// <summary>
    /// How far away the camera should be (z-)
    /// </summary>
    public float distance = 400;
    /// <summary>
    /// The field of view, in degrees
    /// </summary>
    public float fieldOfView = 5;
    /// <summary>
    /// The ease multiplier for fov and distance.
    /// </summary>
    public float dollySpeed = 10;
    /// <summary>
    /// The localposition to dolly towards
    /// </summary>
    public Vector3 targetPosition
    {
        get
        {
            return new Vector3(0, 0, -distance);
        }
    }
    /// <summary>
    /// Dollies and/or zooms
    /// </summary>
    void Update()
    {
        float percent = Time.unscaledDeltaTime * dollySpeed;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fieldOfView, percent);
        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, targetPosition, percent);
    }
}
