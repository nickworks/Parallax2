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
    /// <summary>
    /// Where the player should respawn. This is in local-space.
    /// </summary>
    public Vector3 spawnPosition;
    /// <summary>
    /// The world-space position of where objects should spawn after hitting this checkpoint.
    /// </summary>
    public Vector3 worldSpawnPosition
    {
        get
        {
            return spawnPosition + transform.position;
        }
    }
	/// <summary>
    /// This draws a widget.
    /// </summary>
    void OnDrawGizmos()
    {
        
        Gizmos.DrawCube(worldSpawnPosition, new Vector3(1,2,.1f));
        Gizmos.DrawIcon(worldSpawnPosition + new Vector3(0,2.2f,0), "icon-respawn.png", true);
    }
    /// <summary>
    /// When a collider hits this object, set the object's respawn position in its HealthAndRespawn script.
    /// </summary>
    /// <param name="other">The collider that hit this object.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealthAndRespawn health = other.GetComponent<HealthAndRespawn>();
            if (health) health.SetSpawn(worldSpawnPosition);
            if (health && fillPlayerHP) health.FillHP();
        }
    }
}
