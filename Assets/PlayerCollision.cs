using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

[RequireComponent(typeof(UltEventHolder))]
[RequireComponent(typeof(CompositeCollider2D))]
public class PlayerCollision : MonoBehaviour
{
    private UltEventHolder ultEventHolder;
    private void Start() {
        ultEventHolder = GetComponent<UltEventHolder>();
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        // if layer enemy
        // if tag is bullet
        if (collision.gameObject.tag == "Bullet") {
            ultEventHolder.Invoke();
        }
    }
}
