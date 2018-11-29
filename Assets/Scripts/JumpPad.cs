using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a surface that autojumps the player
/// </summary>
public class JumpPad : MonoBehaviour {
    /// <summary>
    /// Reference to the player controller
    /// </summary>
    private PlayerController playerController;
    /// <summary>
    /// The amount of force behing the jump. Currently set in engine
    /// </summary>
    public float amountOfForce;
    /// <summary>
    /// Raises the trigger enter event.
    /// </summary>
    /// <param name="other">The other object the player collides with</param>
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    /// <summary>
    /// When collision happens, and the tag is jumpable, the player auto jumps
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other){

		if(other.gameObject.CompareTag("Jumpable")){			
			Vector3 playerPos = this.transform.position;
			playerPos.y += amountOfForce;
			transform.position = playerPos;
            print(playerController.velocity);
            playerController.velocity = Vector3.zero; 
            playerPos *= .5f;
			}
	}
}