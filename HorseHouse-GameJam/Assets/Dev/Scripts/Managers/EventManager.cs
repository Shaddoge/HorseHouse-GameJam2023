using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    private static EventManager instance;
    public static EventManager Instance { get { return instance; } }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #region Game
    public event Action<GameState> gameStateChange;
    public void GameStateChange(GameState state) { gameStateChange?.Invoke(state); }
    #endregion

}
