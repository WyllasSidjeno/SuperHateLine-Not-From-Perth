using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    [SerializeField]
    private Camera _Cam;

    // TODO: Don't duplicate code
    private Vector3 mousePosition {
        get {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPoint = _Cam.ScreenToWorldPoint(mousePos);
            worldPoint.z = 0f;
            return worldPoint;
        }
    }

    // Update is called once per frame
    void Update() {
        transform.up = mousePosition - transform.position;
    }
}
