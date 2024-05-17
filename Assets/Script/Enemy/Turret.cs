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
        mTargetrb = _Target.GetComponent<Rigidbody2D>();
    }

    private void OnValidate() {
        if (GetComponentInChildren<Shootable>() == null) {
            var shooter = new GameObject("Gun", typeof(Shootable));
            shooter.transform.parent = transform;
            shooter.transform.localPosition = Vector3.zero;
        }
    }

    /**
     * Shortcut for passing the parameters
     */
    private Vector3 PredictTarget() {
        return Predictive.PredictTarget(
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
        Vector3 pos = Predictive.PredictTarget(
            _Offset.position,
            _Target.transform.position,
            _Target.GetComponent<Rigidbody2D>().velocity,
            GetComponentInChildren<Shootable>()._BulletSpeed
        );
        Gizmos.DrawSphere(pos, 0.25f);
    }
}
