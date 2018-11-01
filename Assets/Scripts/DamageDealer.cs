using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will make an object deal damage on overlap and on hit with its collider.
/// </summary>
public class DamageDealer : MonoBehaviour {
    /// <summary>
    /// How much damage to deal.
    /// </summary>
    public float damageAmount = 25;

    /// <summary>
    /// This message is called when a collider hits this object's collider.
    /// </summary>
    /// <param name="info">The collision info</param>
    void OnCollisionEnter(Collision info) // this doesn't work for the player
    {
        //print("hit damage");
        DoDamage(info.collider.gameObject);
    }
    /// <summary>
    /// This message is called when a collider overlaps this objects collider.
    /// </summary>
    /// <param name="other">The other collider</param>
	void OnTriggerEnter(Collider other) // this works for the player
    {
        //print("overlap damage");
        DoDamage(other.gameObject);
    }
    /// <summary>
    /// Cause this object to damage another object.
    /// </summary>
    /// <param name="obj">Which GameObject to damage. To work, the object must have a HealthAndRespawn behavior.</param>
    void DoDamage(GameObject obj)
    {
        HealthAndRespawn health = obj.GetComponent<HealthAndRespawn>();
        if (health)
        {
            health.TakeDamage(damageAmount);
        }
    }
}
