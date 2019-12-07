using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events
{
    //Game Manager
    [System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> { }
    [System.Serializable] public class SceneLoadCompleted : UnityEvent { }
}
