using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeVolume : MonoBehaviour
{

    /// <summary>
    /// The color of the wireframe applied to the mesh 
    /// </summary>
    public Color color = Color.cyan;

    public bool showInEditor = true;

    [Range(0.0f, 1.0f)]
    public float alpha = 0.5f;

    /// <summary>
    /// we will use this to refrence the box collider atteched to this
    /// </summary>
    BoxCollider boxCollider;

    public Bounds ColliderBounds {//C#  property

        get {
            if (boxCollider == null)
            {
                boxCollider = GetComponent<BoxCollider>();
            }

            return boxCollider.bounds;
        }
    }

    void OnValidate()
    {
        //print("huh?");
        transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Draws the wireframe baised on the collision bounds
    /// </summary>
    void OnDrawGizmos()
    {
        if (!showInEditor) {
            color.a = 0;
        } else {
            color.a = alpha;
        }

        Mesh visualizeCollision = SetVisualisationMesh(ColliderBounds);

        Gizmos.color = color;
        Matrix4x4 temp = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;// Matrix4x4.Rotate(transform.rotation) * Gizmos.matrix;
        //Gizmos.DrawWireMesh(visualizeCollision);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = temp;
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
        int[] triangles = new int[] { 0, 1, 3, 3, 2, 0 };

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
