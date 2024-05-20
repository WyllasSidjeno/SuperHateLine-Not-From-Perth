using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Shooter))]
public class MainMenuShooter : MonoBehaviour
{
    private Shooter shooter;
    private Shootable shootable;
    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<Shooter>();
        shootable = shooter.Gun;
        shooter.enabled = false;
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
   IEnumerator Shoot() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(0.1f, 2.5f));
            shootable.Shoot();
        }
    }
}
