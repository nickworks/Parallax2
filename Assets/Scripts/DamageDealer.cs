using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {

    public float damageAmount = 25;

    void OnCollisionEnter(Collision info) // this doesn't work for the player
    {
        //print("hit damage");
        DoDamage(info.collider.gameObject);
    }
	void OnTriggerEnter(Collider other) // this works for the player
    {
        //print("overlap damage");
        DoDamage(other.gameObject);
    }
    void DoDamage(GameObject obj)
    {
        HealthAndRespawn health = obj.GetComponent<HealthAndRespawn>();
        if (health)
        {
            health.TakeDamage(damageAmount);
        }
    }
}
