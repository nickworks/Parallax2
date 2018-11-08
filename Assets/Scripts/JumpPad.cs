using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

    public float bounceForce;

 
void OnCollisionEnter(Collision hit) {
        if (hit.gameObject.tag == "Player")
        {
            hit.rigidbody.AddForce(bounceForce * Vector3.up, ForceMode.VelocityChange);
        }
    }
}
