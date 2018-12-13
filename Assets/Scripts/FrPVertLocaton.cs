using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider))]
public class FrPVertLocaton : MonoBehaviour {

    //public Camera sceneCam;

    /// <summary>
    /// An intiger label used to make it easier ro understand which corner we are talking about
    /// </summary>
    public enum Corners {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
    }

    /// <summary>
    /// the locations of the front four verticies
    /// </summary>
    public Vector3[] cornerLocations = new Vector3[4];


    /// <summary>
    /// the attached box collider
    /// </summary>
    BoxCollider boxCollider;

    /// <summary>
    /// the bounds of the colider attached to this object
    /// </summary>
    public Bounds bounds {//C#  property

        get {
            if (boxCollider == null) {
                boxCollider = GetComponent<BoxCollider>();
            }

            return boxCollider.bounds;
        }
    }

    /// <summary>
    /// this function initalises this class
    /// </summary>
    void Start()
    {
       
        
    }


	/// <summary>
    /// this function is called every frame
    /// </summary>
	void Update () {
        SetCorners();
    }


    /// <summary>
    /// Sets the location of the corners of this object
    /// </summary>
    void SetCorners() {

        for (int i = 0;i <=3;i++) {
           // cornerLocations[i].corner = (Corners)i;
            //print(cornerLocations[i].corner);
            cornerLocations[i] = GetBoundsVert((Corners)i);
           // print(cornerLocations[i].worldSpacePosition);
        }

    }

    /// <summary>
    /// Returns the worldspace location of the desierd corner
    /// </summary>
    /// <param name="corner"> The corner index of the corner we want to locate according to Corners</param>
    /// <returns>the woreldspace location of the desires corner</returns>
    Vector3 GetBoundsVert(Corners corner) {

        if (corner == Corners.TopLeft) {
            return new Vector3( bounds.min.x, bounds.max.y, bounds.min.z);
        } else if (corner == Corners.TopRight) {
            return new Vector3( bounds.max.x, bounds.max.y, bounds.min.z);
        } else if (corner == Corners.BottomLeft) {
            return new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        } else if (corner == Corners.BottomRight) {
            return new Vector3( bounds.max.x, bounds.min.y, bounds.min.z);
        }

        return Vector3.zero;
    }

    ///public Corner getCorner() { } 



}
