using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UltEventHolder))]
public class MenuUtilitary : MonoBehaviour
{
    UltEventHolder ult;

    void Start() {
        // if the current scene is game or end menu
        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "End Menu") {
            ult = GetComponent<UltEventHolder>();
            ult.Invoke();
        }
        

    }
    public void OnNewGame() {
        // Todo : Delegate and event and shit for the on black
        SceneManager.LoadScene("Game");
    }

    public void OnQuit() {
        // Quit the game
        Application.Quit();
    }
}
