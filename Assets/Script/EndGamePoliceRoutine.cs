using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EndGamePoliceRoutine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ColorObject());

    }

    private IEnumerator ColorObject() {
        while (true) {
            // Change to red
            GetComponent<TextMeshProUGUI>().color = Color.red;
            yield return new WaitForSeconds(0.5f);

            // Change to blue
            GetComponent<TextMeshProUGUI>().color = Color.blue;
            yield return new WaitForSeconds(0.5f);

            // Change to green
            GetComponent<TextMeshProUGUI>().color = Color.green;
            yield return new WaitForSeconds(0.5f);

            // Change to yellow
            GetComponent<TextMeshProUGUI>().color = Color.yellow;
            yield return new WaitForSeconds(0.5f);

            // Change to white
            GetComponent<TextMeshProUGUI>().color = Color.white;
            yield return new WaitForSeconds(0.5f);

        }
    }
}
