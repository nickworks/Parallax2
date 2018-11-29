using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Give this script a target, and it will follow the targeted object.
/// </summary>
public class EaseToTarget : MonoBehaviour {

    /// <summary>
    /// should the camera ease to target durring edit mode
    /// </summary>
    public bool snapDurringEdit = false;

    /// <summary>
    /// The thing to chase
    /// </summary>
    public Transform target;
    /// <summary>
    /// This is an ease multiplier for lerping
    /// </summary>
    public float moveEasing = 10;
	/// <summary>
    /// Move!
    /// </summary>
	void Update () {
        if (!target) return;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * moveEasing);
	}

    /// <summary>
    /// snap the camera to the player durring edit mode
    /// </summary>
    private void OnValidate() {
        if (snapDurringEdit) {
            transform.position = target.position;
           }
    }
}
