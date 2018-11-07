using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    /// <summary>
    /// How fast this projectile moves forward.
    /// </summary>
    public float velocity = 10;
    /// <summary>
    /// Amount of frames before this projectile is destroyed.
    /// </summary>
    public float lifeTime = 10;
	
	void Start () {
		
	}
	void Update () {
        //Move the projectile forward at the velocity stated time deltaTime.
        transform.localPosition += Vector3.right * velocity * Time.deltaTime;
        //Decrement the lifeTime value.
        lifeTime--;
        //If the lifetime value is less or equal to 0, destroy the projectile.
        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
