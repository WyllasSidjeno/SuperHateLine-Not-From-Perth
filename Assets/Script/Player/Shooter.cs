using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    [SerializeField]
    private Shootable _Gun;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            if (_Gun.Shoot()) {
                GetComponent<Animator>().SetTrigger("Shoot");
            }
        }
    }
}
