using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyZoom : MonoBehaviour {

    public Camera cam;

    public float distance = 400;
    public float fieldOfView = 5;
    public float dollySpeed = 10;

    public Vector3 targetPosition
    {
        get
        {
            return new Vector3(0, 0, -distance);
        }
    }

    void Update()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fieldOfView, Time.unscaledDeltaTime * dollySpeed);
        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, targetPosition, Time.unscaledDeltaTime * dollySpeed);
    }
}
