using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    public GameEvents buttonEvent;
    public void TriggerEvent() { GameEvent.RunEvent(buttonEvent); }
}
