using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    private static Action OnCharacterSelect;
    private static Action OnConnectoToServer;
    private static Action OnJoinGameWorld;


    public static void RunEvent(GameEvents _event)
    {
        switch (_event)
        {
            case GameEvents.SelectCharacter: OnCharacterSelect?.Invoke(); break;
            case GameEvents.ConnectToServer: OnConnectoToServer?.Invoke(); break;
            case GameEvents.JoinGameWorld: OnJoinGameWorld?.Invoke(); break;
            
            
            default: throw new ArgumentOutOfRangeException(nameof(_event), _event, null);
        }
    }


}

public enum GameEvents
{
    SelectCharacter,
    ConnectToServer,
    JoinGameWorld,
}