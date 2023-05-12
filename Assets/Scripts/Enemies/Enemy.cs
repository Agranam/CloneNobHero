using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationLerpRate = 3f;
    [SerializeField] private float _attackPeriod = 1f;
    [SerializeField] private float _dps = 10f;

    private PlayerHealth _playerHealth;
    private float _attackTimer;

    private void Update()
    {
        if (_playerHealth != null)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _attackPeriod)
            {
                _playerHealth.TakeDamage(_dps * _attackPeriod);
                _attackTimer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_playerTransform)
        {
            Vector3 toPlayer = _playerTransform.position - transform.position;

            Quaternion toPlayerRotation = Quaternion.LookRotation(toPlayer, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toPlayerRotation, Time.deltaTime * _rotationLerpRate);

            _rigidbody.velocity = transform.forward * _speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            _playerHealth = playerHealth;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>())
        {
            _playerHealth = null;
        }
    }
}