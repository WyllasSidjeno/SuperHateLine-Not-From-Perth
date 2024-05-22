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
    void Update() {
        if (enemyHolder != null) {
            int nbOfObject = enemyHolder.transform.childCount;
            enemyMesh.text = string.Format(
                "{0} traitor{1} left",
                nbOfObject,
                nbOfObject == 1 ? "s" : ""
            );
        }
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
