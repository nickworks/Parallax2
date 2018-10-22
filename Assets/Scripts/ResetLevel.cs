using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This behavior listens for the reset input to be pressed. If it's pressed, the level is reloaded.
/// </summary>
public class ResetLevel : MonoBehaviour
{
    /// <summary>
    /// This reloads the level
    /// </summary>
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// This listens for the "Reset" input to be pressed
    /// </summary>
    void Update () {
        if (Input.GetButtonDown("Reset")) ReloadLevel();
    }
}
