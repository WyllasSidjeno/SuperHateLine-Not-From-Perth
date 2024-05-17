using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimAtTarget : MonoBehaviour {
    [SerializeField]
    [NullMeansSelf]
    private Transform _Offset;

    [SerializeField]
    [Min(0f)]
    private float _DeadZone = 1f;

    private bool mInDeadZone = false;

    [SerializeField]
    [Range(0f, 1f)]
    private float _LerpSpring = 0.5f;

    public void LookAt(Vector3 target) {
        Vector2 targetDir = target - _Offset.position;
        float deadDeadZone = 0.2f;
        float angleToTarget;

        // The deadzone itself has a deadzone
        if (mInDeadZone && targetDir.magnitude > _DeadZone * (1f + deadDeadZone)) {
            mInDeadZone = false;
        }
        else if (!mInDeadZone && targetDir.magnitude < _DeadZone * (1f - deadDeadZone)) {
            mInDeadZone = true;
        }

        if (mInDeadZone) {
            // Do normal rotation
            targetDir = target - transform.position;
        }

        angleToTarget = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        angleToTarget += Quaternion.Angle(_Offset.rotation, transform.rotation);

        var rotation = Quaternion.AngleAxis(angleToTarget, Vector3.forward);
        var lerped = Quaternion.Lerp(transform.rotation, rotation, _LerpSpring);

        transform.rotation = lerped;
    }
}
