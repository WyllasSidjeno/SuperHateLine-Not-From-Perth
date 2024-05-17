using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Turret : MonoBehaviour {
    [SerializeField]
    private GameObject _Target;

    [SerializeField]
    private Transform _Offset;

    private AimAtTarget mAimer;
    private Shootable mShooter;
    private Animator mAnimator;
    private Rigidbody2D mTargetrb;

    private void Start() {
        mAimer = GetComponent<AimAtTarget>();
        mShooter = GetComponentInChildren<Shootable>();
        mAnimator = GetComponent<Animator>();
        mTargetrb = GetComponent<Rigidbody2D>();
    }

    private void OnValidate() {
        if (GetComponentInChildren<Shootable>() == null) {
            var shooter = new GameObject("Gun", typeof(Shootable));
            shooter.transform.parent = transform;
            shooter.transform.localPosition = Vector3.zero;
        }
    }

    private Vector3 PredictTarget(
            Vector3 startPos,
            Vector3 targetPos, Vector3 targetVel,
            float bulletVel
    ) {

        float a = Vector3.Dot(targetVel, targetVel) - bulletVel * bulletVel;
        float b = 2f * Vector3.Dot(targetVel, targetPos - startPos);
        float c = Vector3.Dot(targetPos - startPos, targetPos - startPos);

        float discriminant = b * b - 4f * a * c;

        // If we were to shoot in the past, just shoot at the player
        if (discriminant < 0) {
            return targetPos;
        }

        // t is the time it will take for the bullet to reach the player
        float t = 2f * c / Mathf.Sqrt(discriminant - b);

        // We can now predict where the player will be in t seconds
        Debug.Log((t, targetVel));
        return targetPos + targetVel * t;
    }

    /**
     * Shortcut for passing the parameters
     */
    private Vector3 PredictTarget() {
        return PredictTarget(
            transform.position,
            _Target.transform.position,
            mTargetrb.velocity,
            mShooter._BulletSpeed
        );
    }

    // Update is called once per frame
    private void FixedUpdate() {
        // Predict where the player will be

        var target = PredictTarget();
        mAimer.LookAt(target);

        Vector2 dir = target - _Offset.position;

        // Raycast to see if the turret can shoot at the predicted position
        // TODO: Don't hardcode player layer
        var hit = Physics2D.Raycast(_Offset.position, dir, dir.magnitude);
        var PlayerLayer = LayerMask.NameToLayer("Player");
        if (hit.collider == null || hit.collider.gameObject.layer == PlayerLayer) {
            if (mShooter.Shoot()) {
                //mAnimator.SetTrigger("Shoot");
            }
        }
    }

    private void OnDrawGizmos() {
        // Show the predicted position in the scene
        Gizmos.color = Color.red;
        Vector3 pos = PredictTarget(
            _Offset.position,
            _Target.transform.position,
            _Target.GetComponent<Rigidbody2D>().velocity,
            GetComponentInChildren<Shootable>()._BulletSpeed
        );
        Gizmos.DrawSphere(pos, 0.25f);
    }
}
