using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrPVertLocaton : MonoBehaviour {

    public Camera sceneCam;

    
    /// <summary>
    /// the locations of the front four verticies
    /// </summary>
    Vector3[] vertLocations = new Vector3[4];

    /// <summary>
    /// we will use this variable to get the Mesh on tbis object
    /// </summary>
    Mesh _mesh; //C# private C# feild

    /// <summary>
    /// The color of the wireframe applied to the mesh 
    /// </summary>
    public Color color = Color.cyan;

    // Mesh 

    /// <summary>
    /// get's the mesh when mesh is refrenced
    /// </summary>
    public Mesh mesh {//C#  property

        get {
            if (_mesh == null) _mesh = GetComponent<MeshFilter>().mesh;// "lazy" initailisation
            return _mesh;
        }
    }

    /// <summary>
    /// this function initalises this class
    /// </summary>
    void Start()
    {
       //mesh = GetComponent<Mesh> 
    }


	/// <summary>
    /// this function is called every frame
    /// </summary>
	void Update () {
		
	}


}
