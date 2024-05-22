using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

// Requite ULT Event Holder
[RequireComponent(typeof(UltEventHolder))]

public class SewerTrigger : MonoBehaviour
{
    [SerializeField]
    UltEventHolder ultEventHolder;
    float time;
    void Start() {
        ultEventHolder = GetComponent<UltEventHolder>();
        // store the current time
        time = Time.time;
    }

    [SerializeField]
    GameObject EnemyContainer;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(UIAdapter.EnemyCount == 0)
        {
            Debug.Log("Sewer trigger was called and is valid.");
            // Change scene
            ultEventHolder.Invoke();
            float gameTime = Time.time - time;
            PlayerPrefs.SetFloat("GameTime", gameTime);
            PlayerPrefs.SetInt("EnemyCount", UIAdapter.EnemyCount);

        }
        else
        {
            Debug.Log("Sewer trigger was called and is invalid.");
        }
        
    }
}
