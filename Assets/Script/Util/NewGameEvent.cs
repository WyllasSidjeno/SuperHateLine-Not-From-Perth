using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameEvent : MonoBehaviour
{

    [SerializeField]
    private UltEvents.UltEvent _MyEvent;

    public void invoke(){ _MyEvent.Invoke();}
}
