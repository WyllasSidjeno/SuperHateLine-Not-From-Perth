using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuperHot : MonoBehaviour {

    [SerializeField]
    [Min(0f)]
    private float _LerpTimeSlow = 10;

    [SerializeField]
    [Min(0f)]
    private float _LerpTimeSpeed = 40;

    [SerializeField]
    [Min(0f)]
    private float _SlowFactor = 40;

    [SerializeField]
    private AudioSource _AudioSource;

    // Start is called before the first frame update
    void Start() {
    }

    private void Update() {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        bool hasInput = (x != 0 || y != 0);

        float time = hasInput ? 1f :_SlowFactor;
        float lerpTime = hasInput ? _LerpTimeSpeed : _LerpTimeSlow;

        //time = action ? 1 : time;
        //lerpTime = action ? .1f : lerpTime;
        //Debug.Log(Time.timeScale);
        TimeManager.scale = Mathf.Lerp(Time.timeScale, 1f / time, 1f / lerpTime);
        _AudioSource.pitch = TimeManager.scale;
    }

    // Update is called once per frame
    void FixedUpdate() {
        //var rb = GetComponent<Rigidbody2D>();
        //var vel = rb.velocity.magnitude;
        //Debug.Log(Time.fixedDeltaTime);
        //vel = Mathf.Min(vel + 0.1f, 5f);
        //TimeManager.scale = vel / 5f;
    }
}
