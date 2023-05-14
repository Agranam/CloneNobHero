using UnityEngine;

public class LoseState : GameState
{
    [SerializeField] private LoseWindow _loseWindow;
    
    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0;
        _loseWindow.Show();
    }
}
