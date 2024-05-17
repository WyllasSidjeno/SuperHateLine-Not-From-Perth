using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class Killable : MonoBehaviour {
    [SerializeField]
    private List<Sprite> deathSprites = new List<Sprite>();

    [SerializeField]
    private SpriteRenderer _SpriteRenderer;

    [SerializeField]
    private List<Behaviour> _ToDisable = new List<Behaviour>();

    [SerializeField]
    private UnityEvent _OnDie;

    [SerializeField]
    private LayerMask _KillLayers;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void Die(Collision2D collision) {
        var incoming = collision.collider.gameObject;
        if ((_KillLayers & (1 << incoming.layer)) != 0) {
            // TODO: Make this not jank
            if (incoming.name != "Player") {
                Debug.Log("AAA");
                _SpriteRenderer.sprite = deathSprites.OrderBy(i => Random.value).First();
                _ToDisable.ForEach(i => i.enabled = false);
                _OnDie.Invoke();
            }
        }
    }
}