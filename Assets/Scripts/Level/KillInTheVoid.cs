using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class kills / destroys an object if it falls below a specified y value.
/// If the object has a HealthAndRespawn behavior call its Die() function,
/// otherwise Destroy this object.
/// </summary>
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
            //print("fell into void...");
            HealthAndRespawn health = GetComponent<HealthAndRespawn>();

            if (health)     health.Die();
            else            Destroy(gameObject);            
        }
    } // ends Update()
}
