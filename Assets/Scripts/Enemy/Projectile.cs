using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float velocity = 10;
    public float lifeTime = 15;
	
	void Start () {
		
	}
	void Update () {
        //transform.localPosition += new Vector3(velocity * Time.deltaTime, 0, 0);
        transform.localPosition += Vector3.right * velocity * Time.deltaTime;
        lifeTime--;

        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
