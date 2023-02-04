using JetBrains.Annotations;
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
    public event Action<int> takeDamage;
    public void TakeDamage(int health) { takeDamage?.Invoke(health); }
    public event Action<int> updateHealth;
    public void UpdateHealth(int health) { updateHealth?.Invoke(health); }
    public event Action<int,Vector2> enemyDeath;
    public void EnemyDeath(int progress, Vector2 position) { enemyDeath?.Invoke(progress, position); }
    #endregion

}
