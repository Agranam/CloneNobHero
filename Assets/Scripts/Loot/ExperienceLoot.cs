using UnityEngine;

public class ExperienceLoot : Loot
{
    [SerializeField] private int _experienceValue = 1;

    public void SetExperienceValue(int value)
    {
        _experienceValue = value;
    }
    
    protected override void Take(PlayerCollector playerCollector)
    {
        playerCollector.TakeExperience(_experienceValue);
        base.Take(playerCollector);
    }
}
