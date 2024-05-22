using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.UI;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class RoamingJM : MonoBehaviour
{
    [SerializeField]
    private Transform m_Parent;

    private void Start() {
        StartCoroutine(MoveObject());
    }


    // Update is called once per frame
    private IEnumerator MoveObject()
    {

        // Make it move from left to right
        while (true) {
            // Move to the right
            while (transform.position.x < m_Parent.position.x + 5) {
                transform.position += new Vector3(0.1f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }

            // Move to the left
            while (transform.position.x > m_Parent.position.x - 5) {
                transform.position -= new Vector3(0.1f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }



    }
}
