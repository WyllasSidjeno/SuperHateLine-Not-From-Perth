using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager {
    public static float scale {
        get => Time.timeScale;
        set {
            Time.timeScale = value;
            Time.fixedDeltaTime = Time.timeScale * (1f/50f);
        }
    }
}
