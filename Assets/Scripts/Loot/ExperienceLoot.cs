using UnityEngine;

public class ExperienceLoot : Loot
{
    [SerializeField] private float _experienceValue = 1;

    public void SetExperienceValue(float value)
    {
        _experienceValue = value;
    }
    
    protected override void Take(PlayerCollector playerCollector)
    {
        playerCollector.TakeExperience(_experienceValue);
        base.Take(playerCollector);
    }
}
