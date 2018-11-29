using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a surface that autojumps the player
/// </summary>
public class JumpPad : MonoBehaviour {
    /// <summary>
    /// The velocity to use when bouncing the player
    /// </summary>
    public Vector3 jumpVelocity = new Vector3(0, 35, 0);

    /// <summary>
    /// When collision happens, and the tag is player, and if the collision volume is a trigger, the player auto jumps
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        Bounce(other);
    }
    /// <summary>
    /// This message is called when a collider overlaps this objects collider.
    /// </summary>
    /// <param name="other">The other collider</param>
    void OnTriggerEnter(Collider other) // this works for the player
    {
        Bounce(other);
    }
    /// <summary>
    /// Bounce the player, if this collider is the player.
    /// </summary>
    /// <param name="other">The collider that may (or may not) be on the player object.</param>
    private void Bounce(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.isTrigger == true)
            {
                PlayerController playerController = other.GetComponent<PlayerController>();
                playerController.ResetVelocity();
                playerController.Impulse(jumpVelocity);
            }
        }
    }
}