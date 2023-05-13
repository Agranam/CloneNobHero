using UnityEngine;

public class GameState : MonoBehaviour
{

    protected GameStateManager _gameStateManager;

    public virtual void Init(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }


    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }
}
