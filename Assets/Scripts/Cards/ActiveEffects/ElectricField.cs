using System;
using UnityEngine;

public class ElectricField : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private float _pullRadius;
    private float _damage;

    private float _timer;
    
    public void Init(float radius, float damage)
    {
        _pullRadius = radius;
        _damage = damage;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 0.2f)
        {
            DoEffect();
            _timer = 0;
        }
    }

    private void DoEffect()
    {
        var colliders = Physics.OverlapSphere(transform.position, _pullRadius, _layerMask);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
