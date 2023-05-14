using System;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    private Enemy _targetEnemy;
    private float _speed;
    private float _damage;

    public void SetUp(Enemy enemy, float speed, float damage)
    {
        _targetEnemy = enemy;
        _damage = damage;
        _speed = speed;
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        if (_targetEnemy)
        {
            transform.position = Vector3.MoveTowards
                (transform.position,
                _targetEnemy.transform.position,
                _speed * Time.deltaTime);
            if (transform.position == _targetEnemy.transform.position)
            {
                AffectEnemy();
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void AffectEnemy()
    {
        _targetEnemy.TakeDamage(_damage);
    }
}
