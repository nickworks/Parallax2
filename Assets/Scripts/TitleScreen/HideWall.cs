using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWall : MonoBehaviour {

    public GameObject wall;

	void OnTriggerEnter (Collider other)
    {
        Debug.Log("sup");

        wall.SetActive(false);
    }
}
