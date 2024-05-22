using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour {
    [SerializeField]
    [Min(0)]
    [Tooltip("The maximum number of bullets that can be held at once.")]
    private int _BaseAmmo;
    public int BaseAmmo {
        get { return _BaseAmmo; }
    }



    [SerializeField]
    [Min(0)]
    [Tooltip("The number of times you can shoot per second. 0.5 would be once per 2 seconds.")]
    private float _FireRate = 1;

    [SerializeField]
    [Min(0)]
    [Tooltip("The number of bullets that are shot per shot.")]
    private int _BulletsPerShot = 1;

    [SerializeField]
    [Min(0)]
    [Tooltip("The spread of the shot if multiple bullets are shot.")]
    private float _BulletSpread = 0;

    [SerializeField]
    [Min(0)]
    [Tooltip("The speed of the bullets.")]
    public float _BulletSpeed = 10;

    [SerializeField]
    [Tooltip("The sound that is played when the gun is shot.")]
    private AudioClip _ShootSound;

    [SerializeField]
    [Tooltip("The bullet prefab that is shot.")]
    private Bullet _Bullet;

    [SerializeField]
    [Tooltip("Particle emitter for the empty casings.")]
    private ParticleSystem _CasingParticles;

    [SerializeField]
    private UltEvents.UltEvent _ShootEvent;

    [SerializeField]
    private UltEvents.UltEvent _ReloadEvent;

    [SerializeField]
    public UltEvents.UltEvent _PickupEvent;

    public bool doEvents;

    public bool isPlayer;
    public int mCurrentAmmo { get; private set; }

    private float mLastShotTime;
    private bool mHasReloaded;

    // get for base ammo


    // Start is called before the first frame update
    void Start() {
        mCurrentAmmo = _BaseAmmo;
        mLastShotTime = Time.time - 1 / _FireRate;  // Allow the player to shoot immediately
        var main = _CasingParticles.main;
            main.loop = false;
    }

    public bool CanShoot() {
        return mCurrentAmmo > 0 && Time.time - mLastShotTime >= 1 / _FireRate;
    }

    public bool Shoot() {
        if (!CanShoot()) return false;
        mLastShotTime = Time.time;
        mHasReloaded = false;
        --mCurrentAmmo;
        for (int i = 0; i < _BulletsPerShot; ++i) {
            Bullet bullet = Instantiate(_Bullet, transform.position, transform.rotation);
            bullet.speed = _BulletSpeed;
            bullet.isPlayerBullet = isPlayer;
            if (isPlayer) {
                bullet.gameObject.layer = LayerMask.NameToLayer("Player Bullet");
            }
            else {
                bullet.gameObject.layer = LayerMask.NameToLayer("Enemy Bullet");
            }
            bullet.transform.Rotate(0, 0, Random.Range(-_BulletSpread, _BulletSpread));
        }
        if (_ShootSound != null) {
            AudioSource.PlayClipAtPoint(_ShootSound, transform.position);
        }
        if (_CasingParticles != null) {
            _CasingParticles.Play();
        }

        if (doEvents) {
            _ShootEvent.Invoke();
        }
        return true;
    }

    private void Update() {
        if (!mHasReloaded && CanShoot()) {
            mHasReloaded = true;
            if (doEvents) {
                _ReloadEvent.Invoke();
            }
        }
    }
}
