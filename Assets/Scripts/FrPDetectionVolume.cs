using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class FrPDetectionVolume : MonoBehaviour
{

    /// <summary>
    /// The diffrent configurations of detection that a puzzle peice may take
    /// </summary>
    [Serializable]
    public enum PuzzleFit
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        FourCorners,
        BottomCorners,
        TopCorners,
        LeftCorners,
        RightCorners

    }

    /// <summary>
    /// contains the information that makes p a puzzle peice
    /// </summary>
    [Serializable]
    public struct PuzzlePiece
    {
        public GameObject piece;
        public PuzzleFit fit;
        [HideInInspector]
        public FrPVertLocaton fRpVertLocations;
        [HideInInspector]
        public Vector3[] vertLocations;
        [HideInInspector]
        public bool solved;
    }

    /// <summary>
    /// Contains the informaion that makes up a corner
    /// </summary>
    [HideInInspector]
    public struct Corner
    {
        public Vector3 worldSpacePosition;
        public PuzzleFit corner;
    }

    /// <summary>
    /// The corners on this detection object
    /// </summary>
    Corner[] cornerLocations = new Corner[4];

    /// <summary>
    /// the volume inside of whitch we will look for the solution to our FrP puzzle
    /// </summary>
    Bounds detectoinVolume;

    /// <summary>
    /// the layer fixed object attechae to this
    /// </summary>
    LayerFixed layerFixed;

    /// <summary>
    /// the leway we give the player on the puzzle
    /// </summary>
    public static float buffer = 2;

    /// <summary>
    /// used to visuaisualise the leway we give the player
    /// </summary>
    Vector3 _BufferMod {

        get {
            return new Vector3(buffer / 2, buffer / 2, 0);
        }

    }

    /// <summary>
    /// the puzzle peices that this is looking for
    /// </summary>
    public PuzzlePiece[] pieces;

    /// <summary>
    /// we will use this to refrence the box collider atteched to this
    /// </summary>
    BoxCollider boxCollider;

    /// <summary>
    /// Whether we should set the corner locations for this object
    /// </summary>
    bool setCorners = true;

    /// <summary>
    /// A getter for the bounds attached to this object
    /// </summary>
    public Bounds bounds {//C#  property

        get {
            if (boxCollider == null)
            {
                boxCollider = GetComponent<BoxCollider>();
            }

            return boxCollider.bounds;
        }
    }

    /// <summary>
    /// This draws the detection bounds of this object
    /// </summary>
    void OnDrawGizmos()
    {
        DrawDetection();
    }




    // Use this for initialization
    void Start()
    {
        layerFixed = GetComponent<LayerFixed>();

        for (int i = pieces.Length - 1; i >= 0; i--)
        {
            pieces[i].fRpVertLocations = pieces[i].piece.GetComponent<FrPVertLocaton>();
            pieces[i].vertLocations = pieces[i].fRpVertLocations.cornerLocations;
            pieces[i].solved = false;
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        SetCorners();


        for (int i = pieces.Length - 1; i >= 0; i--)
        {
            pieces[i].solved = SolutionComparinator(pieces[i]);
        }


        if (PuzzleSolved()) print("Puzzle Solved, " + pieces[0].fit);

        //DrawDetection();
        //print();
    }

    bool PuzzleSolved() {
        int s = 0;
        foreach (PuzzlePiece p in pieces)
        {
            if (p.solved) {
                s++;
            }
        }

        return pieces.Length == s;
    }

    /// <summary>
    /// This determins if a piece is in the aproptiate location ot solve a puzzle according to it's assighned fit and worldspace location
    /// </summary>
    /// <param name="puzzlePeice">the puzzle peice we are checking</param>
    /// <returns>if the peice is in the proper location</returns>
    bool SolutionComparinator(PuzzlePiece puzzlePeice)
    {

        switch (puzzlePeice.fit)
        {
            case PuzzleFit.TopLeft:
                if (ScreenSeperation(cornerLocations[(int)PuzzleFit.TopLeft].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.TopLeft)])) return true;
                break;
            case PuzzleFit.TopRight:
                if (ScreenSeperation(cornerLocations[(int)PuzzleFit.TopRight].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.TopRight)])) return true;
                break;
            case PuzzleFit.BottomLeft:
                if (ScreenSeperation(cornerLocations[(int)PuzzleFit.BottomLeft].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.BottomLeft)])) return true;
                break;
            case PuzzleFit.BottomRight:
                if (ScreenSeperation(cornerLocations[(int)PuzzleFit.BottomRight].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.BottomRight)])) return true;
                break;
            case PuzzleFit.FourCorners:
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.TopLeft].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.TopLeft)])) return false;
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.TopRight].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.TopRight)])) return false;
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.BottomRight].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.BottomRight)])) return false;
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.BottomLeft].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.BottomLeft)])) return false;
                return true;

            case PuzzleFit.BottomCorners:
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.BottomRight].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.BottomRight)])) return false;
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.BottomLeft].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.BottomLeft)])) return false;
                return true;

            case PuzzleFit.TopCorners:
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.TopLeft].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.TopLeft)])) return false;
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.TopRight].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.TopRight)])) return false;
                return true;

            case PuzzleFit.LeftCorners:
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.TopLeft].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.TopLeft)])) return false;
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.BottomLeft].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.BottomLeft)])) return false;
                return true;

            case PuzzleFit.RightCorners:
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.TopRight].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.TopRight)])) return false;
                if (!ScreenSeperation(cornerLocations[(int)PuzzleFit.BottomRight].worldSpacePosition, puzzlePeice.vertLocations[(int)(PuzzleFit.BottomRight)])) return false;
                return true;

            default:
                print("ERROR: puzzle fit out of bounds in FrPDetectionVolume");
                break;

        }

        return false;
    }

    /// <summary>
    /// This function takes two spoints in world space and gets their seperation in screen space (2D) and then sees if they are close enough to have solved the puzzle
    /// </summary>
    /// <param name="pointOne">The first point in world space we want to pass in</param>
    /// <param name="pointTwo">The second point in world space we want to pass in</param>
    /// <returns>The distence between the two specified points in screen space</returns>
    bool ScreenSeperation(Vector3 pointOne, Vector3 pointTwo)
    {
        Vector3 a = CameraController.main.dolly.cam.WorldToScreenPoint(pointOne);
        Vector3 b = CameraController.main.dolly.cam.WorldToScreenPoint(pointTwo);

        //print("a: " + a + " b: " + b);

        float dist = Vector2.Distance(a, b);

        return dist < buffer;
    }



    void DrawDetection() {
        

        Gizmos.color = Color.red;
        Gizmos.DrawWireMesh(SetVisualisationMesh(PuzzleFit.TopLeft));
        Gizmos.DrawWireMesh(SetVisualisationMesh(PuzzleFit.TopRight));
        Gizmos.DrawWireMesh(SetVisualisationMesh(PuzzleFit.BottomLeft));
        Gizmos.DrawWireMesh(SetVisualisationMesh(PuzzleFit.BottomRight));
    }

    //Color 




    /// <summary>
    /// Sets the location of the corners of this object
    /// </summary>
    void SetCorners()
    {
        if (setCorners && transform.position.z == layerFixed.z * LayerFixed.separation)
        {

            for (int i = 0; i <= 3; i++)
            {
                cornerLocations[i].corner = (PuzzleFit)i;
                //print(cornerLocations[i].corner);
                cornerLocations[i].worldSpacePosition = GetBoundsVert((PuzzleFit)i);
                //print(cornerLocations[i].worldSpacePosition);
            }
            setCorners = false;
        }
    }


    /// <summary>
    /// Returns the worldspace location of the desierd corner
    /// </summary>
    /// <param name="corner"> The corner index of the corner we want to locate according to PuzzleFit</param>
    /// <returns>the woreldspace location of the desires corner</returns>
    Vector3 GetBoundsVert(PuzzleFit corner)
    {

        if (corner == PuzzleFit.TopLeft)
        {
            return new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        }
        else if (corner == PuzzleFit.TopRight)
        {
            return new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        }
        else if (corner == PuzzleFit.BottomLeft)
        {
            return new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        }
        else if (corner == PuzzleFit.BottomRight)
        {
            return new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        }

        return Vector3.zero;
    }


    /// <summary>
    /// This function creates a wireframe plane baised on the detection volume. It does so along the XY axis it is drawn in the center of the object. 
    /// </summary>
    /// <returns>The mesh visualising detectionVolume</returns>
    Mesh SetVisualisationMesh(PuzzleFit corner)
    {
        if ((int)corner > 3) return null;

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

        for (int i = vertices.Length - 1; i >= 0; i--)
        {
            vertices[i] = GetBoundsVert(corner);
            
        }

        print(vertices[0] + "," + vertices[1] + "," + vertices[2] + "," + vertices[3]);

        print(_BufferMod);

        vertices[0] +=  new Vector3( -_BufferMod.x, _BufferMod.y, 0);
        vertices[1] += new Vector3( _BufferMod.x, _BufferMod.y, 0);
        vertices[2] += new Vector3( -_BufferMod.x, -_BufferMod.y, 0);
        vertices[3] += new Vector3( _BufferMod.x, -_BufferMod.y, 0);

        print(vertices[0] + "," + vertices[1] + "," + vertices[2] + "," + vertices[3]);
        
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
