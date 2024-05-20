using System.Collections;
using UltEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UltEventHolder))]
public class GameHandler : MonoBehaviour {
    public void OnValidEndGameTrigger() {
        Debug.Log("End game trigger was called and is valid.");
        StartCoroutine(ChangeSceneAfterDelay(4f));
    }

    private IEnumerator ChangeSceneAfterDelay(float delay) {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene("End Menu");
    }
}
