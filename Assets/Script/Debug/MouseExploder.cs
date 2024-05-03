using UnityEngine;
using UnityEngine.Rendering;

public class MouseExploder : MonoBehaviour {
    public ExplosionForce explosion;
    [SerializeField]
    private Camera _Cam;

    private Vector3 mousePosition {
        get {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0f;
            return _Cam.ScreenToWorldPoint(mousePos);
        }
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Instantiate(explosion, mousePosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmos() {
        if (Input.GetButton("Fire1")) {
            Gizmos.DrawWireSphere(mousePosition, explosion._Radius);
        }
    }
}
