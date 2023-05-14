using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action OnDie;
    
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    private void Start()
    {
        SetHealth(_maxHealth);
    }

    public void TakeDamage(float value)
    {
        float newHealth = _currentHealth - value;
        newHealth = Mathf.Max(newHealth, 0);
        SetHealth(newHealth);
        if (newHealth == 0)
        {
            Die();
        }
    }

    private void SetHealth(float value)
    {
        _currentHealth = value;
        _healthBar.SetValueBar(_currentHealth, _maxHealth);
    }

    private void Die()
    {
        OnDie?.Invoke();
    }
}
