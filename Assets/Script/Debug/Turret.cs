using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turret : MonoBehaviour {
    [SerializeField]
    private GameObject _Target;

    [SerializeField]
    private Transform _Offset;

    [SerializeField]
    private float _DeadZone = 1f;

    private bool mInDeadZone = false;

    [SerializeField]
    [Range(0f, 1f)]
    private float _LerpSpring = 0.5f;

    // Update is called once per frame
    private void FixedUpdate() {
        Vector2 dir = _Target.transform.position - _Offset.position;
        float deadDeadZone = 0.2f;
        float angleToMouse;

        // The deadzone itself has a deadzone
        if (mInDeadZone && dir.magnitude > _DeadZone * (1f + deadDeadZone)) {
            mInDeadZone = false;
        }
        else if (!mInDeadZone && dir.magnitude < _DeadZone * (1f - deadDeadZone)) {
            mInDeadZone = true;
        }

        if (mInDeadZone) {
            // Do normal rotation
            dir = _Target.transform.position - transform.position;
        }

        angleToMouse = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        var rotation = Quaternion.AngleAxis(angleToMouse, Vector3.forward);
        var lerped = Quaternion.Lerp(transform.rotation, rotation, _LerpSpring);

        transform.rotation = lerped;

        // Raycast to see if the turret can see the player
        var hit = Physics2D.Raycast(_Offset.position, dir, dir.magnitude);
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (GetComponentInChildren<Shootable>().Shoot()) {
                GetComponent<Animator>().SetTrigger("Shoot");
            }
        }
    }
}
