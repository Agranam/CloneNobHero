using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Player _player;

    private void Awake()
    {
        _player.OnHealthChange += SetValueBar;
    }

    private void OnDisable()
    {
        _player.OnHealthChange -= SetValueBar;
    }

    private void SetValueBar(float current, float max)
    {
        _healthBar.fillAmount = current / max;
    }

}
