using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CloudManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _CloudPrefab;

    private Rigidbody2D rb;
    private BoxCollider2D bc;

    [SerializeField] private bool left = false;

    [SerializeField]
    private float m_Speed = 1.0f;

    [SerializeField]
    private float _CloudSpawnRate = 1.0f;

    [SerializeField]
    private float _MinCloudSpawnRate = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();

        StartCoroutine(SpawnClouds());

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // destroy tje collisionned item
        Destroy(collision.gameObject);
    }

    IEnumerator SpawnClouds() {
        while (true) {
            // Randomize the spawn rate of the clouds
            float curr_spawn_rate = Random.Range(_MinCloudSpawnRate, _CloudSpawnRate);
            yield return new WaitForSeconds(curr_spawn_rate);

            // Randomize the y position of the cloud based from the min max y of this object
            float y = Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
            Vector2 spawnPosition = new Vector2(transform.position.x, y);

            // Add the width of the cloud to the spawn position
            spawnPosition.x += left ? 20 : -20;

            GameObject cloud = Instantiate(_CloudPrefab, spawnPosition, Quaternion.identity);

            // Go from right to left only.
            if (left) {
                cloud.GetComponent<Rigidbody2D>().velocity = new Vector2(m_Speed, 0);
            } else {
                cloud.GetComponent<Rigidbody2D>().velocity = new Vector2(-m_Speed, 0);
            }
        }
    }
}
