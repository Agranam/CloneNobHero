using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth.OnHealthChange += SetValueHealthBar;
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChange -= SetValueHealthBar;
    }

    private void SetValueHealthBar(float current, float max)
    {
        _healthBar.fillAmount = current / max;
    }

}
