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
    private TextMeshProUGUI bullet_mesh;


    // Update is called once per frame
    void Update()
    {
        if (bulletScript != null)
        {
            bullet_mesh.text =
                string.Format("[{0}/{1}]",
                bulletScript.CurrentAmmo,
                bulletScript.BaseAmmo
                );
        }
    }
}
