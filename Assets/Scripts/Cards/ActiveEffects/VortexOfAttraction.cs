using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VortexOfAttraction : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform _transformParent;
    [SerializeField] private float _pullRadius = 4f;
    
    private float _pullForce;
    private float _throwForce;
    private float _damage;
    private float _timeToAction = 3f;
    private bool _isFinish;

    void FixedUpdate()
    {
        if (_timeToAction > 0)
        {
            _timeToAction -= Time.fixedDeltaTime;
            AttractionEffect();
        }
        else if(_timeToAction <= 0 && !_isFinish)
        {
            StartCoroutine(FinishEffect());
            _isFinish = true;
        }
    }

    public void SetUp(float radius, float force, float throwForce, float damage, float timeToAction)
    {
        _pullRadius = radius;
        _pullForce = force;
        _throwForce = throwForce;
        _damage = damage;
        _timeToAction = timeToAction;
        _transformParent.localScale = Vector3.one * _pullRadius;
    }
    
    private IEnumerator FinishEffect()
    {
        var colliders = Physics.OverlapSphere(transform.position, _pullRadius, _layerMask);

        List<Enemy> enemiesForDamage = new();
        
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemiesForDamage.Add(enemy);
            }
        }

        foreach (var enemy in enemiesForDamage)
        {
            enemy.Jump(0.2f, 3);
        }

        yield return new WaitForSeconds(0.3f);
        
        foreach (var enemy in enemiesForDamage)
        {
            enemy.TakeDamage(_damage);
        }
        
        Destroy(gameObject, 0.2f);
    }

    private void AttractionEffect()
    {
        _particleSystem.Play();
        var main = _particleSystem.main;
        main.simulationSpeed += 0.02f;
        
        var colliders = Physics.OverlapSphere(transform.position, _pullRadius, _layerMask);

        foreach (Collider collider in colliders)
        {
            if (!collider.GetComponent<Enemy>()) return;

            Rigidbody rb = collider.GetComponent<Rigidbody>();

            //Vector3 forceDirection = transform.position - collider.transform.position;

            Vector3 directionToCenter = (transform.position - collider.transform.position).normalized;
            rb.AddForce(directionToCenter * _pullForce);
            //rb.AddForce(forceDirection.normalized * _pullForce * Time.fixedDeltaTime);
        }
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.grey;
        Handles.DrawWireDisc(transform.position, Vector3.up, _pullRadius);
    }
#endif
}
