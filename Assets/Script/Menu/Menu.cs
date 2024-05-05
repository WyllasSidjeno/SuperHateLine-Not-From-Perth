using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnNewGame() {
        // Todo : Delegate and event and shit for the on black
        SceneManager.LoadScene("Game");
    }

    public void OnQuit() {
        // Quit the game
        Application.Quit();
    }
}
