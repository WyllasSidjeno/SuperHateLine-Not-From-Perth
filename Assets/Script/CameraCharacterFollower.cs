using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraCharacterFollower : MonoBehaviour{
    private Camera _camera;
    private Transform _transform;
    [SerializeField] private GameObject _follower;
    Vector3 _position = new Vector3(0, 0, -10);

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _transform = _camera.transform;
        _position = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _position.x = _follower.transform.position.x;
        _position.y = _follower.transform.position.y;

        _transform.position = _position;
    }
}
