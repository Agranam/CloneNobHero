using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private float _distanceToCollect;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ExperienceManager _experienceManager;
    
    private void FixedUpdate()
    {
        PickUpLoot();
    }

    public void PickUpLoot()
    {
        Collider[] colliders = Physics.OverlapSphere
            (transform.position, _distanceToCollect, 
                _layerMask, QueryTriggerInteraction.Ignore);
        
        foreach (var collider in colliders)
        {
            if(collider.TryGetComponent(out Loot loot))
            {
                loot.Collect(this);
            }
        }
    }

    public void TakeExperience(int value)
    {
        _experienceManager.AddExperience(value);
    }
}
