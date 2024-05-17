using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour {
    [SerializeField]
    [Min(0)]
    [Tooltip("The maximum number of bullets that can be held at once.")]
    private int _BaseAmmo;
    public int BaseAmmo
    {
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

    public bool isPlayer;
    private int mCurrentAmmo;
    public int CurrentAmmo
    {
        get { return mCurrentAmmo; }
    }

    private float mLastShotTime;

    // get for base ammo


    // Start is called before the first frame update
    void Start() {
        mCurrentAmmo = _BaseAmmo;
        mLastShotTime = Time.time - 1 / _FireRate;  // Allow the player to shoot immediately
    }

    public bool Shoot() {
        if (mCurrentAmmo == 0 || Time.time - mLastShotTime < 1 / _FireRate) {
            return false;
        }
        mLastShotTime = Time.time;
        --mCurrentAmmo;
        for (int i = 0; i < _BulletsPerShot; ++i) {
            Bullet bullet = Instantiate(_Bullet, transform.position, transform.rotation);
            bullet.speed = _BulletSpeed;
            bullet.isPlayerBullet = isPlayer;
            if (isPlayer) {
                bullet.gameObject.layer = LayerMask.NameToLayer("Player");
            } else {
                bullet.gameObject.layer = LayerMask.NameToLayer("Enemy");
            }
            bullet.transform.Rotate(0, 0, Random.Range(-_BulletSpread, _BulletSpread));
        }
        if (_ShootSound != null) {
            AudioSource.PlayClipAtPoint(_ShootSound, transform.position);
        }
        return true;
    }
}
