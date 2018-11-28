using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public Vector3 pointB;
    public Vector3 incritement;

	// Use this for initialization
	IEnumerator Start () {
        Vector3 pointA = transform.position;
        while(true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));

        }

    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
	}
    /*
    IEnumerator RotateObject(Transform thisTransform, Vector3 startRot, Vector3 newRot)
    {
        float i = 0.0f;
        newRot =(startRot += incritement);
        while (i < 1.0f)
        {
            i += Time.deltaTime;
            thisTransform.rotation.x = newRot.x;
            yield return null;
        }
    }
    */
}
