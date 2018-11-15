using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Test makes it so when a button is activated, it moves an object (like a Door) up, then lowers it once the button is released.
/// </summary>
public class ButtonDoor : MonoBehaviour {
    /// <summary>
    /// Stores how far to move the object.
    /// </summary>
    public int moveDistance = 0;
    /// <summary>
    /// When called, this function moves the GameObject "up" two meters in the World Space.
    /// </summary>
	public void ButtonActivate ()
    {
        //print("Button has been activated.");
        transform.Translate(new Vector3(0, moveDistance, 0), Space.World);
    }
    /// <summary>
    /// When called, this function moves the GameObject "down" two meters in the World Space.
    /// </summary>
    public void ButtonDeactivate ()
    {
        //print("Button has been deactivated.");
        transform.Translate(new Vector3(0, -moveDistance, 0), Space.World);
    }
}
