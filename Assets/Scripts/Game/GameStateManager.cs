using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private GameState _currentGameState;
    
    [SerializeField] private GameState _menuState;
    [SerializeField] private GameState _actionState;
    [SerializeField] private GameState _pauseState;
    [SerializeField] private GameState _winState;
    [SerializeField] private GameState _loseState;
    [SerializeField] private GameState _cardState;

    public void Init()
    {
        _menuState?.Init(this);
        _actionState?.Init(this);
        _pauseState?.Init(this);
        _winState?.Init(this);
        _loseState?.Init(this);
        _cardState?.Init(this);

        SetGameState(_menuState);
    }

    private void OnEnable()
    {
        Player.OnDie += SetLose;
    }
    
    private void OnDisable()
    {
        Player.OnDie -= SetLose;
    }
    
    private void SetGameState(GameState gameState)
    {
        if(_currentGameState)
        {
            _currentGameState.Exit();
        }
        _currentGameState = gameState;

        gameState.Enter();
    }

    public void SetMenu()
    {
        SetGameState(_menuState);
    }
    public void SetAction()
    {
        SetGameState(_actionState);
    }
    public void SetPause()
    {
        SetGameState(_pauseState);
    }
    public void SetWin()
    {
        SetGameState(_winState);
    }
    public void SetLose()
    {
        SetGameState(_loseState);
    }
    public void SetCardState()
    {
        SetGameState(_cardState);
    }
}
