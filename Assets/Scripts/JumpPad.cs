using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Creates a surface that autojumps the player
/// </summary>
public class JumpPad : MonoBehaviour {
	/// <summary>
	/// The amount of force behing the jump. Currently set in engine
	/// </summary>
	public float amountOfForce;
	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">The other object the player collides with</param>
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Jumpable")){			
			Vector3 playerPos = this.transform.position;
			playerPos.y += amountOfForce;
			transform.position = playerPos;
			print (transform.position.y);
			}
	}
}