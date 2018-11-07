using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Our top-level game state for Parallax2.
/// </summary>
public static class Px2 {

    /// <summary>
    /// Whether or not the game is paused.
    /// </summary>
    static public bool paused { get; private set; }
	static public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
    }
    static public void Unpause()
    {
        paused = false;
        Time.timeScale = 1;
    }
}
