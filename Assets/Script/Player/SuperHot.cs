using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuperHot : MonoBehaviour {

    [SerializeField]
    [Min(0f)]
    private float _LerpTimeSlow = 10f;

    [SerializeField]
    [Min(0f)]
    private float _LerpTimeSpeed = 40f;

    [SerializeField]
    [Min(0f)]
    private float _SlowFactor = 40f;

    [SerializeField]
    [Min(0f)]
    private float _AudioSlowFactor = 2f;

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
        float scale = Mathf.Lerp(Time.timeScale, 1f / time, 1f / lerpTime);
        TimeManager.scale = scale;

        float audioPitchScale = Mathf.Clamp(scale, 0f, 1f) * _AudioSlowFactor;
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources) {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, audioPitchScale, 0.01f);
        }
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
