﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this script onto a collider to make a checkpoint!
/// </summary>
public class Checkpoint : MonoBehaviour {

    public Vector3 spawnPosition;
    public Vector3 worldSpawnPosition
    {
        get
        {
            return spawnPosition + transform.position;
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnDrawGizmos()
    {
        Gizmos.DrawCube(worldSpawnPosition, Vector3.one * .1f);
    }
    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            // checkpoint!
            other.GetComponent<HealthAndRespawn>().SetSpawn(worldSpawnPosition);
        }
    }
}