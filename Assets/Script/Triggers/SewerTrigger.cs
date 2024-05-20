using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

// Requite ULT Event Holder
[RequireComponent(typeof(UltEventHolder))]

public class SewerTrigger : MonoBehaviour
{

    UltEventHolder ultEventHolder;
    void Start() {
        ultEventHolder = GetComponent<UltEventHolder>();
    }

    [SerializeField]
    GameObject EnemyContainer;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(EnemyContainer.transform.childCount <= 0)
        {
            Debug.Log("Sewer trigger was called and is valid.");
            // Change scene
            ultEventHolder.Invoke();
        }
        else
        {
            Debug.Log("Sewer trigger was called and is invalid.");
        }
        
    }
}
