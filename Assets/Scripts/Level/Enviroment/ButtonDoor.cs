using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a test script applied to an object to test various buttons. It has the object act as a door
/// by opening and closing like a door when the buttons are activated.
/// </summary>
public class ButtonDoor : MonoBehaviour {
    /// <summary>
    /// Stores how far to move the object when the button is activated/deactivated.
    /// </summary>
    public int moveDistance = 0;
    /// <summary>
    /// When called, this function moves the GameObject "up" a set moveDistance in the World Space.
    /// </summary>
	public void DoorOpen ()
    {
        //print("Button has been activated.");
        transform.Translate(new Vector3(0, moveDistance, 0), Space.World);
    }
    /// <summary>
    /// When called, this function moves the GameObject "down" a set moveDistance in the World Space.
    /// </summary>
    public void DoorClose ()
    {
        //print("Button has been deactivated.");
        transform.Translate(new Vector3(0, -moveDistance, 0), Space.World);
    }
}
