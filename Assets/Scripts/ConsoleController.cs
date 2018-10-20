using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ConsoleController : MonoBehaviour {

    public delegate bool CommandDelegate(string cmd);
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
        DontDestroyOnLoad(gameObject);
        MakeCommands();
        output.supportRichText = true;
        main = this;
    }
    /// <summary>
    /// This function is called when ever the text changes in the input field.
    /// </summary>
    /// <param name="val">The new text in the input field</param>
	public void TextUpdate(string val)
    {

    }
    /// <summary>
    /// This function is called whenever the input field is submitted (when the user presses enter, or clicks elsewhere).
    /// </summary>
    /// <param name="cmd">The current text in the input field</param>
    public void TextSubmit(string cmd)
    {
        cmd = cmd.Trim(new char[]{' ', '`'});
        if (cmd.Length > 0)
        {
            string name = GetCommandName(cmd);
            logInput(cmd);
            if (commands.ContainsKey(name))
            {
                if (commands[name].Invoke(cmd))
                {

                }
                else
                {

                }
            }
            else
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
    }
    /// <summary>
    /// Create all of the commands!
    /// </summary>
    void MakeCommands()
    {
        commands["test"] = (string str)=>{
            
            log("hello world");
            return true;
        };
        string text = "commands loaded: ";
        foreach (string k in commands.Keys) text += k + ", ";
        log(text);
    }
}
