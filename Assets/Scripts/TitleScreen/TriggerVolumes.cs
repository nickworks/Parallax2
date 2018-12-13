using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolumes : MonoBehaviour {
    /// <summary>
    /// An integer for the index that corresponds to the scene index in the build settings
    /// </summary>
    public int sceneIndex;
    /// <summary>
    /// creates a new ChangeScenes object
    /// </summary>
    ChangeScenes changer = new ChangeScenes();

	void OnTriggerEnter(Collider other)
    {
        if (sceneIndex >= 0) changer.ChangeToScene(sceneIndex); // if the index is 0 or higher, change to that particular scene
        if (sceneIndex == -1) Application.Quit(); //if the index is set to -1, quit the application
    }
}
