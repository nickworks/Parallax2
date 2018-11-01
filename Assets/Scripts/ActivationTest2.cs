using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Add Comments
/// <summary>
/// This Test makes it so when a button is activated, it moves an object (like a Door) up, then lowers it once the button is released.
/// </summary>
public class ActivationTest2 : MonoBehaviour {
    /// <summary>
    /// When called, this function moves the GameObject "up" two meters in the World Space.
    /// </summary>
	void Activate ()
    {
        transform.Translate(new Vector3(0, 2, 0), Space.World);
    }
    /// <summary>
    /// When called, this function moves the GameObject "down" two meters in the World Space.
    /// </summary>
    void Deactivate ()
    {
        transform.Translate(new Vector3(0, -2, 0), Space.World);
    }
}
