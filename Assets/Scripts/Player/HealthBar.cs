using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private bool _lookToCamera;
    [SerializeField] private Image _healthBar;

    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if(!_lookToCamera) return;
        
        transform.rotation = _camera.transform.rotation;
    }

    public void SetValueBar(float current, float max)
    {
        _healthBar.fillAmount = current / max;
    }
}
