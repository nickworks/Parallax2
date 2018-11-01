using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO comment what this class does
public class KillInTheVoid : MonoBehaviour {

    /// <summary>
    /// If this object falls below this y value, it will automatically die.
    /// </summary>
    public float yKillThreshold = -10;

    /// <summary>
    /// Checks if the object has fallen past the yKillThreshold. If so, kill the object.
    /// </summary>
    void Update()
    {
        if (transform.position.y < yKillThreshold)
        {

            HealthAndRespawn health = GetComponent<HealthAndRespawn>();

            if (health)
            {
                health.Die();
            } else
            {
                Destroy(gameObject);
            }
            
        }
    } // ends Update()
}
