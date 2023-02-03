using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum GameState
{
    None,
    Paused
}

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameState state;
    public GameState State =>state;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
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
    private void OnEnable()
    {
        EventManager.Instance.gameStateChange += SetState;
    }

    private void SetState(GameState _state)
    {
        state = _state;
    }

    private void OnDisable()
    {
        EventManager.Instance.gameStateChange -= SetState;
    }
}
