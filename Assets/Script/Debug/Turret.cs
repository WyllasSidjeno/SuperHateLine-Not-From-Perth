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

    private Vector3 PredictTarget() {
        Vector2 targetPos = _Target.transform.position;
        Vector2 targetVel = _Target.GetComponent<Rigidbody2D>().velocity;
        Vector2 turretPos = _Offset.position;
        float bulletVel = GetComponentInChildren<Shootable>()._BulletSpeed;

        float a = Vector2.Dot(targetVel, targetVel) - bulletVel * bulletVel;
        float b = 2f * Vector2.Dot(targetVel, targetPos - turretPos);
        float c = Vector2.Dot(targetPos - turretPos, targetPos - turretPos);

        float discriminant = b * b - 4f * a * c;

        // If we were to shoot in the past, just shoot at the player
        if (discriminant < 0) {
            return targetPos;
        }

        // t is the time it will take for the bullet to reach the player
        float t = 2f * c / Mathf.Sqrt(discriminant - b);

        // We can now predict where the player will be in t seconds
        return targetPos + targetVel * t;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        // Predict where the player will be

        var target = PredictTarget();


        Vector2 dir = target - _Offset.position;
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
            dir = target - transform.position;
        }

        angleToMouse = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        var rotation = Quaternion.AngleAxis(angleToMouse, Vector3.forward);
        var lerped = Quaternion.Lerp(transform.rotation, rotation, _LerpSpring);

        transform.rotation = lerped;

        // Raycast to see if the turret can shoot at the predicted position
        var hit = Physics2D.Raycast(_Offset.position, dir, dir.magnitude);
        var PlayerLayer = LayerMask.NameToLayer("Player");
        if (hit.collider == null || hit.collider.gameObject.layer == PlayerLayer) {
            if (GetComponentInChildren<Shootable>().Shoot()) {
                GetComponent<Animator>().SetTrigger("Shoot");
            }
        }
    }

    private void OnDrawGizmos() {
        // Show the predicted position in the scene
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(PredictTarget(), 0.25f);
    }
}
