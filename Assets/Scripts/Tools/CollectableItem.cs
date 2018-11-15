using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// code to control different types of collectable items and the player's interaction with them
/// </summary>
public class CollectableItem : MonoBehaviour {
    /// <summary>
    /// is this object a key collectable?
    /// </summary>
    public bool isKey = false;
    /// <summary>
    /// is this object a coin collectable?
    /// </summary>
    public bool isCoin = false;

    /// <summary>
    /// </summary>
    /// <param name="info"></param>
    void OnTriggerEnter(Collider collider)
    {
        //if (collider.tag == "Danger") print("danger moved into you");
        OnHitGameObject(collider);
    }
    /// <summary>
    /// What to do if colliding with another?
    /// Checks the tag on the other collider.
    /// </summary>
    /// <param name="obj">The other collider</param>
    void OnHitGameObject(Collider obj)
    {
        
        if (obj.tag == "Player")
        {
            //add one to the px2 numcollect variable
            if(isCoin) Px2.numCoin++;
            if (isKey) Px2.numKeys++;
            Destroy(this.gameObject);
        }

    }
}
