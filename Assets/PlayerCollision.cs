using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

[RequireComponent(typeof(UltEventHolder))]
[RequireComponent(typeof(CompositeCollider2D))]
public class PlayerCollision : MonoBehaviour {
    private float startTime;
    private UltEventHolder ultEventHolder;

    private void Start() {
        ultEventHolder = GetComponent<UltEventHolder>();
        startTime = Time.unscaledTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            float elapsedTime = Time.unscaledTime - startTime;  // Calculate the elapsed time
            PlayerPrefs.SetFloat("GameTime", elapsedTime);
            PlayerPrefs.SetInt("EnemyCount", UIAdapter.EnemyCount);
            ultEventHolder.Invoke();
        }
    }
}
