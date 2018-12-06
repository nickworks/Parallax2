using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

	// Use this for initialization
	private Rigidbody rb;
	public float fallDelay = 1f;
	int speed = 5;
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
	void OnTriggerEnter(Collider other)
	{
		print ("Collided");
		if (other.tag == "Player")
		{
			print ("Colluded with player");
			Invoke ("Fall", fallDelay);
		}
	}

	void Fall()
	{
		print ("I'm using gravity!");
		rb.useGravity = true;
	}
}
	