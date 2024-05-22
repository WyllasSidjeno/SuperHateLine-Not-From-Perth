using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnStart : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    TextMeshProUGUI subtitle;
    void Start()
    {
        // Put the timescale to 1
        Time.timeScale = 1;

        float gameTime = PlayerPrefs.GetFloat("GameTime");
        int enemyCount = PlayerPrefs.GetInt("EnemyCount");
        int totalSeconds = Mathf.RoundToInt(gameTime);

        string message = $"There was {enemyCount} left. You lasted {totalSeconds} second{(totalSeconds == 1 ? "" : "s")}.";
        subtitle.text = message;




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
