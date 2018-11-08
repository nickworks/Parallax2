using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this script pushes all rigidbodies that the character touches
/// </summary>
public class PushBlock : MonoBehaviour {

    /// <summary>
    /// the amount that we push the rigid body
    /// </summary>
    public float pushPower = 2.0f;

    /// <summary>
    /// This logic runs when the player is moving and collides with something. It allowes the player to push rigid bodies. https://docs.unity3d.com/ScriptReference/CharacterController.OnControllerColliderHit.html
    /// </summary>
    /// <param name="hit"></param>
    void OnControllerColliderHit(ControllerColliderHit hit) {

        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic) {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3) {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }

    /*
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/
}
