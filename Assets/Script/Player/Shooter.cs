using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    [SerializeField]
    private Shootable _Gun;

    [SerializeField]
    [Tooltip("The parent where picked up guns are set")]
    private Transform _ShooterParent;

    [SerializeField]
    private UltEvents.UltEvent _OnPickup;

    [SerializeField]
    private Camera _Cam;

    public Shootable Gun {
        get { return _Gun; }
        set { 
            _Gun = value;
            Destroy(_ShooterParent.GetChild(0).gameObject);
            value.transform.parent = _ShooterParent.transform;
            value.transform.localPosition = Vector3.zero;
            value.transform.localRotation = Quaternion.identity;
            value.GetComponent<Collider2D>().enabled = false;
            value.GetComponent<SpriteRenderer>().enabled = false;
            value.GetComponent<Rigidbody2D>().simulated = false;
            value.GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
            value.GetComponentInChildren<Canvas>().worldCamera = _Cam;
            value.isPlayer = true;
            value.doEvents = true;

            _Gun._PickupEvent.Invoke();
        }
    }


    // Update is called once per frame
    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            if (Gun.Shoot()) {
                GetComponent<Animator>().SetTrigger("Shoot");
            }
        }
    }

    public void Pickup(Collider2D coll) {
        Shootable newGun = coll.GetComponent<Shootable>();
        Debug.Log(coll.gameObject.name);
        if (newGun != null) {
            Gun = newGun;
            _OnPickup.Invoke();
        }
    }
}
