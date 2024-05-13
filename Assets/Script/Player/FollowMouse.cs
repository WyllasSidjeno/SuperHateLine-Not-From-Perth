using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowMouse : MonoBehaviour {

    [SerializeField]
    private Camera _Cam;

    [SerializeField]
    private Transform _Offset;

    [SerializeField]
    private float _DeadZone = 1f;

    private bool mInDeadZone = false;

    [SerializeField]
    [Range(0f, 1f)]
    private float _LerpSpring = 0.5f;

    // TODO: Don't duplicate code
    private Vector3 mousePosition;

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPoint = _Cam.ScreenToWorldPoint(mousePos);
        //worldPoint.z = 0f;
        mousePosition = worldPoint;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        Vector2 mouseDir = mousePosition - _Offset.position;
        float deadDeadZone = 0.2f;
        float angleToMouse;

        // The deadzone itself has a deadzone
        if (mInDeadZone && mouseDir.magnitude > _DeadZone * (1f + deadDeadZone)) {
            mInDeadZone = false;
        } else if (!mInDeadZone && mouseDir.magnitude <  _DeadZone * (1f - deadDeadZone)) {
            mInDeadZone = true;
        }

        if (mInDeadZone) {
            // Do normal rotation
            mouseDir = mousePosition - transform.position;
        }

        angleToMouse = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg - 90f;

        var rotation = Quaternion.AngleAxis(angleToMouse, Vector3.forward);
        var lerped = Quaternion.Lerp(transform.rotation, rotation, _LerpSpring);

        transform.rotation = lerped;
    }

    private void OnDrawGizmos() {
        // Show the cursor position in the scene
        Gizmos.DrawSphere(mousePosition, 0.05f);
    }
}
