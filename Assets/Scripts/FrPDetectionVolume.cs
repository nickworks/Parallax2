using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

public class FrPDetectionVolume : MonoBehaviour
{
    [Serializable]
    public enum PuzzleFit
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        All
    }

    [Serializable]
    public struct PuzzlePiece
    {
        public GameObject piece;
        public PuzzleFit fit;
    }

    /// <summary>
    /// the volume inside of whitch we will look for the solution to our FrP puzzle
    /// </summary>
    Bounds detectoinVolume;

    /// <summary>
    /// the leway we give the player on the puxzle
    /// </summary>
    Vector3 bufferMod = new Vector3(.5f, .5f, 0);

    public PuzzlePiece[] pieces;
   
    /// <summary>
    /// we will use this to refrence the box collider atteched to this
    /// </summary>
    BoxCollider boxCollider;

    public Bounds vertCollider {//C#  property

        get {
            if (boxCollider == null)
            {
                boxCollider = GetComponent<BoxCollider>();
            }

            return boxCollider.bounds;
        }
    }

    void OnDrawGizmos()
    {
        SetDetectionVolume();
        Mesh visualizeDetectionVolume = SetVisualisationMesh(detectoinVolume);

        Gizmos.color = Color.red;
        Gizmos.DrawWireMesh(visualizeDetectionVolume);
    }




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetDetectionVolume() {
        detectoinVolume = vertCollider;
        detectoinVolume.extents += bufferMod;
    }

    /// <summary>
    /// This function creates a wireframe plane baised on the detection volume. It does so along the XY axis it is drawn in the center of the object. 
    /// </summary>
    /// <returns>The mesh visualising detectionVolume</returns>
    Mesh SetVisualisationMesh(Bounds bounds)
    {
        Mesh vizMesh = new Mesh();

        /*
         * 0: Top Left
         * 1: Top Right
         * 2: Bottom Left
         * 3: Botton Right
         */
        Vector3[] vertices = new Vector3[4];
        //Vector2[] uvs = new Vector2[4];
        int[] triangles = new int[] { 0, 1, 3, 3, 2, 0};

        vertices[0] = new Vector3(bounds.min.x, bounds.max.y, bounds.center.z);
        vertices[1] = new Vector3(bounds.max.x, bounds.max.y, bounds.center.z);
        vertices[2] = new Vector3(bounds.min.x, bounds.min.y, bounds.center.z);
        vertices[3] = new Vector3(bounds.max.x, bounds.min.y, bounds.center.z);
        /*
        for (int i = 0; i < 4; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
        }
        */
        vizMesh.vertices = vertices;
        //vizMesh.uv = uvs;
        vizMesh.triangles = triangles;
        vizMesh.SetNormals(new List<Vector3> { Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward });

        return vizMesh;
    }
}
