using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrPVertLocaton : MonoBehaviour {

    //public Camera sceneCam;

    
    public enum Corners {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
    }

    [HideInInspector]
    public struct Corner {
        public Vector3 worldSpacePosition;
        public Corners corner;
    }

    /// <summary>
    /// the locations of the front four verticies
    /// </summary>
    public Corner[] cornerLocations = new Corner[4];

    /// <summary>
    /// we will use this variable to get the Mesh on tbis object
    /// </summary>
   // Mesh _mesh; //C# private C# feild

    /// <summary>
    /// The color of the wireframe applied to the mesh 
    /// </summary>
    public Color color = Color.cyan;

    /// <summary>
    /// get's the mesh when mesh is refrenced
    /// </summary>
   // public Mesh thisMesh;

    BoxCollider boxCollider;

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
       // thisMesh = GetComponent<MeshFilter>().mesh;
        SetCorners();
    }


	/// <summary>
    /// this function is called every frame
    /// </summary>
	void Update () {
		
	}

    void SetCorners() {

        for (int i = 0;i <=3;i++) {
            cornerLocations[i].corner = (Corners)i;
            print(cornerLocations[i].corner);
            cornerLocations[i].worldSpacePosition = GetBoundsVert((Corners)i);
           // print(cornerLocations[i].worldSpacePosition + " i " + i);
        }

    }

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
