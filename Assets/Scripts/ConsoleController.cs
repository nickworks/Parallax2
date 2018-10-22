using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// A controller that handles all console state and logic.
/// </summary>
public class ConsoleController : MonoBehaviour {

    /// <summary>
    /// A delegate to handle commands in the console.
    /// </summary>
    /// <param name="cmd">The full command</param>
    /// <returns>Returns true if successful.</returns>
    public delegate void CommandDelegate(string cmd);
    /// <summary>
    /// A dictionary of commands. The keys are the one-word command identifiers. The values are delegates (anonymous functions). 
    /// </summary>
    Dictionary<string, CommandDelegate> commands = new Dictionary<string, CommandDelegate>();
    /// <summary>
    /// A reference to the gui
    /// </summary>
    public Transform gui;
    /// <summary>
    /// A reference to the input textfield
    /// </summary>
    public InputField input;
    /// <summary>
    /// Whether or not the input has focus currently.
    /// </summary>
    private bool hasFocus = false;
    /// <summary>
    /// A reference to the output text
    /// </summary>
    public Text output;
    /// <summary>
    /// Where or not the console is currently open
    /// </summary>
    public static bool isConsoleOpen { get; private set; }
    /// <summary>
    /// Singleton access
    /// </summary>
    public static ConsoleController main;
    /// <summary>
    /// Sets up a few important things.
    /// </summary>


    void Start () {
        if (main == null)
        {
            DontDestroyOnLoad(gameObject);
            MakeCommands();
            output.supportRichText = true;
            main = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// This function is called when ever the text changes in the input
    /// </summary>
    /// <param name="val">The new text in the input field</param>
	public void TextUpdate(string val)
    {

    }
    /// <summary>
    /// This function is called whenever the input field is submitted
    /// (when the user presses enter, or clicks elsewhere).
    /// Call this from Update() only. Do not use the On End Edit event in the textfield.
    /// </summary>
    /// <param name="cmd">The current text in the input field</param>
    private void TextSubmit(string cmd)
    {
        cmd = cmd.Trim(new char[]{' ', '`'}); // split on spaces and tildes
        // tilde's make sense, since the only time they'd be
        // added to the textbox is when opening / closing

        if (cmd.Length > 0) // if there's text in the input box:
        {
            logInput(cmd); // log the cmd
            string name = GetCommandName(cmd); // get the command's name
            if (commands.ContainsKey(name)) // if command exists:
            {
                commands[name].Invoke(cmd); // call command
            }
            else // otherwise, log an error:
            {
                logError("command \""+name+"\" not recognized");
            }
        }
        input.text = "";
        input.Select();
        input.ActivateInputField();
    }
    /// <summary>
    /// Adds some text to the console output log.
    /// </summary>
    /// <param name="str">The text to add to the output log</param>
    public void log(string str)
    {
        output.text += "\n"+str;
    }
    /// <summary>
    /// Adds some text to the console output log. This text is formatted to look like input.
    /// </summary>
    /// <param name="str">The text to add to the output log</param>
    public void logInput(string str)
    { 
        output.text += "\n\n<color=white><b>" + str+"</b></color>\n";
    }
    /// <summary>
    /// Adds some text to the console output log. This text is formatted to look like an error.
    /// </summary>
    /// <param name="str">The text to add to the output log</param>
    public void logError(string str)
    {
        output.text += "\n<color=red>" + str + "</color>";
    }
    /// <summary>
    /// Returns the first word of a multi-part command.
    /// For example, if the input were "cd example", this function would return "cd".
    /// </summary>
    /// <param name="val">The full command</param>
    /// <returns>The first word of the command</returns>
    string GetCommandName(string val)
    {
        return CommandPart(val, 0);
    }
    /// <summary>
    /// Splits a command string into parts.
    /// </summary>
    /// <param name="val">The command string to split</param>
    /// <param name="i">Which piece you want. Starts at 0.</param>
    /// <param name="split">The character to split. Default is a space.</param>
    /// <returns></returns>
    string CommandPart(string val, int i, char split = ' ')
    {
        string[] parts = val.Split(split);
        if (i < 0 || i >= parts.Length) return "";
        return parts[i];
    }
    /// <summary>
    /// If the console button is pressed, toggle the console.
    /// If escape is pressed, close the console.
    /// </summary>
	void Update () {
        if (Input.GetButtonDown("Console")) isConsoleOpen = !isConsoleOpen;
        if (Input.GetButtonDown("Cancel")) isConsoleOpen = false;

        if (isConsoleOpen) // show the console:
        {
            ShowConsole();
        }
        else // resume gameplay:
        {
            HideConsole();
        }

        // Check for [enter] to be pressed by the player:
        if (hasFocus && Input.GetKeyDown(KeyCode.Return))
        {
            TextSubmit(input.text);
        }
        hasFocus = input.isFocused;
    }
    /// <summary>
    /// Shows the console gui.
    /// Activates the input textfield.
    /// Pauses the game.
    /// </summary>
    void ShowConsole()
    {
        gui.gameObject.SetActive(true);
        input.Select();
        input.ActivateInputField();
        Px2.Pause();
    }
    /// <summary>
    /// Hides the console gui.
    /// Unpauses the game.
    /// </summary>
    void HideConsole()
    {
        gui.gameObject.SetActive(false);
        Px2.Unpause();
        input.text = "";
    }
    /// <summary>
    /// Create all of the commands!
    /// </summary>
    void MakeCommands()
    {
        // CREATE THE COMMANDS:
        commands["list"] = commands["help"] = (string cmd) =>
        {
            string text = "commands available: ";
            foreach (string key in commands.Keys) text += key + ", ";
            log(text);
        };
        commands["reset"] = commands["reload"] = (string cmd) =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        };
        commands["levels"] = commands["scenes"] = (string cmd) =>
        {
            string result = "available scenes: ";
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneByBuildIndex(i);
                result += scene.name + ", ";
            }
            log(result);
        };
        commands["separation"] = (string cmd) =>
        {
            float amt = FloatVal(CommandPart(cmd, 1));
            if(amt != 0) LayerFixed.separation = amt;
            else log("layer separation: " + LayerFixed.separation);
        };
        commands["fov"] = (string cmd) =>
        {
            float amt = FloatVal(CommandPart(cmd, 1));
            if (amt != 0)
            {
                if(!CameraController.main) logError("no camera found");
                else CameraController.main.dolly.fieldOfView = amt;
            } else
            {
                if (!CameraController.main) logError("no camera found");
                else log("camera fov: " + CameraController.main.dolly.fieldOfView);
            }
        };
        commands["dis"] = (string cmd) =>
        {
            float amt = FloatVal(CommandPart(cmd, 1));
            if (amt != 0)
            {
                if (!CameraController.main) logError("no camera found");
                else CameraController.main.dolly.distance = amt;
            } else
            {
                if (!CameraController.main) logError("no camera found");
                else log("camera distance: "+CameraController.main.dolly.distance);
            }
        };

        commands["load"] = (string cmd)=>{

            string name = CommandPart(cmd, 1).ToLower();
            bool levelFound = false;
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneByBuildIndex(i);
                if (scene.name.ToLower() == name)
                {
                    levelFound = true;
                    SceneManager.LoadScene(scene.name);
                    break;
                }
            }
            if (!levelFound) logError("scene \""+name+"\" not found");
        };

        // DONE LOADING COMMANDS:
        log(commands.Count + " commands loaded\ntype <b>list</b> to see them all");
    }
    float FloatVal(string str)
    {
        float amt = 0;
        float.TryParse(str, out amt);
        return amt;
    }
}
