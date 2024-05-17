using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleMove : MonoBehaviour {
    public float _MoveSpeed;

    private Rigidbody2D mRigidbody;
    private Vector2 mDirection;

    // Start is called before the first frame update
    void Start() {
        mRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        mDirection.x = Input.GetAxisRaw("Horizontal");
        mDirection.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        //mRigidbody.AddForce(mDirection * _MoveSpeed);
        mRigidbody.position += mDirection * _MoveSpeed * Time.deltaTime;
    }
}
