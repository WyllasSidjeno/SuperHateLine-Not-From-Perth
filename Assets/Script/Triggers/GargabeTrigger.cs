using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargabeTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject weapon;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) {
        // If tag is player
        if (other.tag == "Player") {
            Debug.Log("Garbage trigger was called.");

        }
       
    }
}
