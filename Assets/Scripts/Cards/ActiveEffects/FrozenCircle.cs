using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FrozenCircle : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _pullRadius = 3;
    
    private List<Enemy> _frozenEnemies = new();

    public List<Enemy> FrozenEnemies => _frozenEnemies;

    public void SetUp(float radius)
    {
        _pullRadius = radius;
        transform.localScale = Vector3.one * radius;
        _sphereCollider.radius = radius / 3;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if(enemy.IsFrozen) return;
            _frozenEnemies.Add(enemy);
            enemy.Frozen();
        }
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.up, _pullRadius);
    }
#endif
}
