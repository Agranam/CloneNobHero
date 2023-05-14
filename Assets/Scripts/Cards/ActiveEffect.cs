using UnityEngine;

public class ActiveEffect : Effect
{

    [SerializeField] protected float _cooldown;

    protected float _timer;
    
    public void ProcessFrame(float frameTime)
    {
        _timer += frameTime;
        if (_timer > _cooldown)
        {
            Produce();
            _timer = 0;
        }
    }

    protected virtual void Produce()
    {
        
    }
}