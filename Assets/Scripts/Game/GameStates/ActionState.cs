using UnityEngine;

public class ActionState : GameState
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private ExperienceManager _experienceManager;

    public override void EnterFirstTime()
    {
        base.EnterFirstTime();
        _enemyManager.StartNewWave(0);
        _experienceManager.UpLevel();
    }

    public override void Enter()
    {
        base.Enter();
        _joystick.Activate();
        _playerMove.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        _joystick.Deactivate();
        _playerMove.enabled = false;

    }
}
