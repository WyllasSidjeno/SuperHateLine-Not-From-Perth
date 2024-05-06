using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "MenuCallbacks", menuName = "ScriptableObjects/Menu", order = 1)]
public class Menu2 : ScriptableObject
{
    public void OnNewGame()
    {
        // Todo : Delegate and event and shit for the on black
        SceneManager.LoadScene("Game");
    }

    public void OnQuit()
    {
        // Quit the game
        Application.Quit();
    }
}
