using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

	/// <summary>
    /// The rigidbody componant of the falling platform
    /// </summary>
	private Rigidbody rb;
    /// <summary>
    /// How long we want to wait before the platform falls
    /// </summary>
	public float fallDelay = 1f;
/// <summary>
/// Gets the rigid body
/// </summary>
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
    /// <summary>
    /// When the trigger is triggered, check if it is colliding with the player. If it is, call the fall funtion after the fall delay happens
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Invoke ("Fall", fallDelay);
		}
	}
    /// <summary>
    /// Changes the rigid body to use gravity, making the box fall 
    /// </summary>
	void Fall()
	{
		rb.useGravity = true;
	}
}
	