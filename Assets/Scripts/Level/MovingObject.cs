using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this script allows objects to move between two Vector3 locations, and also rotate along their local axis'
/// </summary>
public class MovingObject : MonoBehaviour {
    /// <summary>
    /// if this object is for spinning
    /// </summary>
    public bool isSpinner;
    /// <summary>
    /// if this object is for moving between twho points
    /// </summary>
    public bool isTransform;
    /// <summary>
    /// tracking if this object is moving from its original position (true) or back from the 2nd postition (false)
    /// </summary>
    private bool fromFirst = true;
    /// <summary>
    /// the starting point of the object
    /// </summary>
    private Vector3 pointA;
    /// <summary>
    /// the ending point of the object
    /// </summary>
    public Vector3 pointB;
    /// <summary>
    /// the amount you want to change the object's rotation by each frame, and also along which axis
    /// </summary>
    public Vector3 newRotateAmount;
    /// <summary>
    /// how long you want it to take to move between point A and point B
    /// </summary>
    public float moveTime = 3;
    /// <summary>
    /// the amount of time the object has been in the animation
    /// </summary>
    public float currentTime = 0;

    
	void Start () {
        pointA = transform.position;
    }

    void Update()
    {
        if (isTransform)
        {
            ObjectMove(transform);
        }
        if (isSpinner)
        {
            transform.Rotate(newRotateAmount * Time.deltaTime);
        }
    }

    /// <summary>
    /// lerp the object's tranform between point A and B
    /// </summary>
    /// <param name="thisTransform"></param>
    private void ObjectMove(Transform thisTransform)
    {
        currentTime += Time.deltaTime;
        float percent = currentTime / moveTime;
        if (fromFirst)
          {
             thisTransform.position = Vector3.Lerp(pointA, pointB, percent);
          }
        if (!fromFirst)
            {
                thisTransform.position = Vector3.Lerp(pointB, pointA, percent);
            }
        if (percent >= 1)
        {
            fromFirst = !fromFirst;
            currentTime = 0;
        }
    }
   
    
}
