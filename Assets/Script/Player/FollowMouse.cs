using UnityEngine;

[RequireComponent(typeof(AimAtTarget))]
public class FollowMouse : MonoBehaviour {
    [SerializeField]
    private Camera _Cam;

    private AimAtTarget mAimer;

    private void Start() {
        mAimer = GetComponent<AimAtTarget>();
    }

    private void Update() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPoint = _Cam.ScreenToWorldPoint(mousePos);
        //worldPoint.z = 0f;
        mAimer.LookAt(worldPoint);
    }

    private void OnDrawGizmos() {
        // Show the cursor position in the scene
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPoint = _Cam.ScreenToWorldPoint(mousePos);

        Gizmos.DrawSphere(worldPoint, 0.05f);
    }
}
