//Any Scene that you want to be loadable needs to be
//added the list of scenes in the Buld Settings

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{

    public void ChangeToScene(int scene)
    {
        SceneManager.LoadScene(scene); //Loads the scene at a given index from Build Settings
    }
}