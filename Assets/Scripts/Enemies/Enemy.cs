using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _dyingEffect;
    [SerializeField] private float _health = 50;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationLerpRate = 3f;
    [SerializeField] private float _attackPeriod = 1f;
    [SerializeField] private float _dps = 10f;

    private EnemyManager _enemyManager;
    private Player _player;
    private Transform _playerTransform;
    private float _attackTimer;
    private float _spawnRadius;

    private void Update()
    {
        if (_player != null)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _attackPeriod)
            {
                _player.TakeDamage(_dps * _attackPeriod);
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

            if (toPlayer.magnitude > _spawnRadius * 2)
            {
                transform.position += toPlayer * 1.95f;
            }
        }
    }

    public void Init(EnemyManager enemyManager, Transform playerTransform, float spawnRadius)
    {
        _enemyManager = enemyManager;
        _playerTransform = playerTransform;
        _spawnRadius = spawnRadius;
    }

    public void DoDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player playerHealth))
        {
            _player = playerHealth;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _player = null;
        }
    }

    private void Die()
    {
        _enemyManager.RemoveEnemy(this);
        Instantiate(_dyingEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}