using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseToTarget : MonoBehaviour {

    public Transform target;

    public float moveEasing = 10;

    void Start () {
		
	}
	
	void Update () {
        if (!target) return;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * moveEasing);
	}
}
