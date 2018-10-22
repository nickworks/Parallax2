using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Give this script a target, and it will follow the targeted object.
/// </summary>
public class EaseToTarget : MonoBehaviour {
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
}
