using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAdapter : MonoBehaviour
{
    // Serialized fild of list type
    [SerializeField]
    private Shootable bulletScript;
    [SerializeField]
    private TextMeshProUGUI bulletMesh;

    [SerializeField]
    private GameObject enemyHolder;
    [SerializeField]
    private TextMeshProUGUI enemyMesh;

    // Update is called once per frame
    void Update()
    {
        if (bulletScript != null)
        {
            bulletMesh.text =
                string.Format("[{0}/{1}]",
                bulletScript.CurrentAmmo,
                bulletScript.BaseAmmo
                );
        }

        if (enemyHolder != null)
        {
            int nbOfObject = enemyHolder.transform.childCount;
            enemyMesh.text = 
                string.Format("{0} traitor{1} left",
                nbOfObject, 
                nbOfObject == 1 ? "s" : ""
                );

        }
    }
}
