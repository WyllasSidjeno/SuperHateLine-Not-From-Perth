using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {
    [SerializeField]
    private Color playerColor;

    [SerializeField]
    private Color enemyColor;


    public bool isPlayerBullet = false;
    public float speed = 10;

    // Start is called before the first frame update
    void Start() {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (isPlayerBullet) {
            trail.startColor = playerColor;
        }
        else {
            trail.startColor = enemyColor;
        }
        trail.endColor = trail.startColor * new Color(1, 1, 1, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // TODO: Add damage to the player or enemy
        Destroy(gameObject);
    }
}
