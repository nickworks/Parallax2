using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationTest2 : MonoBehaviour {

	void Activate ()
    {
        transform.Translate(new Vector3(0, 2, 0), Space.World);
    }

    void Deactivate ()
    {
        transform.Translate(new Vector3(0, -2, 0), Space.World);
    }
}
