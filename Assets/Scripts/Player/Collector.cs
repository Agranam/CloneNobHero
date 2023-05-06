using System;
using UnityEditor;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private float _distanceToCollect;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ExperienceManager _experienceManager;
    [SerializeField] private ParticleSystem _levelUpParticles;

    private void OnEnable()
    {
        ExperienceManager.LevelUp.AddListener(PlayLevelUpEffect);
    }

    private void OnDisable()
    {
        ExperienceManager.LevelUp.RemoveListener(PlayLevelUpEffect);
    }

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

    private void PlayLevelUpEffect()
    {
        _levelUpParticles.Play();
    }
    
    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToCollect);
    }
}
