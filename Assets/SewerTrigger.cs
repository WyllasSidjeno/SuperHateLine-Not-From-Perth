using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject EnemyContainer;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(EnemyContainer.transform.childCount > 0)
        {
            Debug.Log("Sewer trigger was called and is valid.");
        }
        else
        {
            Debug.Log("Sewer trigger was called and is invalid.");
        }
        
    }
}
