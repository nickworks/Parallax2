using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// This class creates a Translate handle for Checkpoints in Editor windows.
/// </summary>
[CustomEditor(typeof(Checkpoint))]
public class CheckpointEditor : Editor {
    /// <summary>
    /// Called when rendering the target Checkpoint in an Editor window.
    /// </summary>
    protected virtual void OnSceneGUI()
    {
        Checkpoint checkpoint = (Checkpoint)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.PositionHandle(checkpoint.worldSpawnPosition, Quaternion.identity); // draw a handle, get its position
        if (EditorGUI.EndChangeCheck()) // if a change is detected...
        {
            Undo.RecordObject(checkpoint, "Moved checkpoint respawn position"); // mark it in the undo
            checkpoint.spawnPosition = newPosition - checkpoint.transform.position; // move the checkpoint
        }
    }
}
