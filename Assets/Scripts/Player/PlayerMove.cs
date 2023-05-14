using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 5f;

    private Vector2 _moveInput;
    private readonly int _run = Animator.StringToHash("Run");

    private void Update()
    {
        _moveInput = _joystick.Value;

        if (_moveInput == Vector2.zero)
        {
            _animator.SetBool(_run, false);
        }
        else
        {
            _animator.SetBool(_run, true);
        }
    }

    public void StopMove()
    {
        _animator.SetBool(_run, false);
        _rigidbody.velocity = Vector3.zero;
    }
    
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_moveInput.x, 0f, _moveInput.y) * _speed;

        if (_rigidbody.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);
        }
    }
}
