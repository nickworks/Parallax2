using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this script onto a collider to make a checkpoint!
/// </summary>
public class Checkpoint : MonoBehaviour {
    /// <summary>
    /// if true, player's hp will be filled when they reach this checkpoint
    /// </summary>
    public bool fillPlayerHP = true;
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
            if (fillPlayerHP)
            {
                other.GetComponent<HealthAndRespawn>().FillHP();
            }
        }
    }
}
