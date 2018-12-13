using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWall : MonoBehaviour {
    /// <summary>
    /// Refence to the gameObject to be affected
    /// </summary>
    public GameObject wall;

	void OnTriggerEnter (Collider other)
    {
        Debug.Log("sup");
        
        wall.SetActive(false); //Deactivate the gameObject when this trigger is hit
    }
}
