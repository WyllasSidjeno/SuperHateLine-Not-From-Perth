using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DumbGangster : MonoBehaviour {
    [SerializeField]
    private GameObject _Target;

    [SerializeField]
    private Transform _ShootPoint;

    [SerializeField]
    [Tooltip("The distance the enemy will try to keep between it and its target.")]
    [Min(0f)]
    private float _TargetRadius;

    [SerializeField]
    [Min(0f)]
    private float _SightRadius;

    [SerializeField]
    [Min(0f)]
    private float _Speed;

    private AimAtTarget mAimer;
    private Shootable mShooter;
    private Rigidbody2D mTargetrb;
    private Rigidbody2D mRigidbody;

    private void Start() {
        mAimer = GetComponent<AimAtTarget>();
        mShooter = GetComponentInChildren<Shootable>();
        mTargetrb = _Target.GetComponent<Rigidbody2D>();
        mRigidbody = GetComponent<Rigidbody2D>();
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
        Vector3 targetDir = _Target.transform.position - transform.position;

        var sight = Physics2D.Raycast(
            transform.position, targetDir, _SightRadius,
            ~(1 << gameObject.layer)  // Collide anything except self or another enemy
        );

        // TODO: Differentiate between player bullets and the player
        if (sight.collider && sight.collider.gameObject.layer == _Target.layer) {
            // Walk to desired distance
            // The position I want my target to be at
            Vector3 targettargetPos = targetDir.normalized * _TargetRadius;
            // How much we have to move to get the target to be there
            var walkDir = (transform.position - (_Target.transform.position - targettargetPos)) * -1;
            walkDir = Vector3.ClampMagnitude(walkDir * _Speed, _Speed);
            mRigidbody.AddForce(walkDir);


            // Predict where the player will be
            var target = PredictTarget();
            mAimer.LookAt(target);

            Vector2 dir = target - _ShootPoint.position;

            // Raycast to see if the turret can shoot at the predicted position
            var hit = Physics2D.Raycast(
                _ShootPoint.position, dir, dir.magnitude,
                ~(1 << gameObject.layer)
            );
            if (hit.collider == null || hit.collider.gameObject.layer == _Target.layer) {
                mShooter.Shoot();
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _SightRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _TargetRadius);
    }
}
