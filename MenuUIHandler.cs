using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        // add code here to handle when a color is selected
        MainManager.instance.TeamColor = color; // Gets reference to main manager instance and team color and sets it to the chosen color
    }

    private void Start()
    {
        ColorPicker.Init();
        // this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;
        ColorPicker.SelectColor(MainManager.instance.TeamColor); // Pre-select the chosen color if the user chose on before, each time. 
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    // If the Unity Editor is running, exit play mode. Otherwise, quit out of the application.
    public void QuitGame()
    {
        MainManager.instance.SaveColor(); // Saves the user's last chosen color when the application is exited

        if (UnityEditor.EditorApplication.isPlaying)
        {
            EditorApplication.ExitPlaymode(); 
        } else
        {
            Application.Quit();
        }
    }

    // Methods so I can save and load the color from the application instantly 
    public void SaveColorClicked()
    {
        MainManager.instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        MainManager.instance.LoadColor();
        ColorPicker.SelectColor(MainManager.instance.TeamColor);
    }
}
