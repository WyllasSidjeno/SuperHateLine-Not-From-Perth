using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAdapter : MonoBehaviour {
    // Serialized fild of list type
    [SerializeField]
    private TextMeshProUGUI bulletMesh;

    [SerializeField]
    private TextMeshProUGUI _GunStatusMesh;

    [SerializeField]
    private GameObject enemyHolder;

    [SerializeField]
    private TextMeshProUGUI enemyMesh;

    // Update is called once per frame
    void Start() {
        if (enemyHolder != null) {
            EnemyCount = enemyHolder.transform.childCount;
        }
    }

    public void UpdateEnemyText() {
         enemyMesh.text = $"{EnemyCount} traitor{(EnemyCount == 1 ? "" : "s")} left";
    }

    // SINGLETON WOO
    public static int EnemyCount = 0;
    public static void DecrementEnemyText() {
        --EnemyCount;
    }

    public void UpdateAmmoText(Shootable gun) {
        bulletMesh.text = $"[{gun.mCurrentAmmo}/{gun.BaseAmmo}]";
    }

    public void UpdateGunStatusText(Shootable gun) {
        if (gun.CanShoot()) {
            _GunStatusMesh.text = "Shoot away!";
        } else if (gun.mCurrentAmmo <= 0) {
            _GunStatusMesh.text = "Out of ammo!";
        } else {
            _GunStatusMesh.text = "Reloading!";
        }
    }
}
