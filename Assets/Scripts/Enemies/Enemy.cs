using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _dyingEffect;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Animator _animator;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Color _frozenColor;
    [SerializeField] private float _fullHealth = 50;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationLerpRate = 3f;
    [SerializeField] private float _attackPeriod = 1f;
    [SerializeField] private float _dps = 10f;
    [SerializeField] private float _expSpawn = 1f;

    private EnemyManager _enemyManager;
    private ExperienceManager _experienceManager;
    private Player _player;
    private Transform _playerTransform;
    private float _attackTimer;
    private float _spawnRadius;
    private float _currentHealth;
    private float _currentSpeed;
    private bool _isFrozen;
    
    public bool IsFrozen => _isFrozen;

    private void Start()
    {
        SetHealth(_fullHealth);
        _currentSpeed = _speed;
    }

    private void Update()
    {
        if(_isFrozen) return;

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
        if(_isFrozen) return;
        
        if (_playerTransform)
        {
            Vector3 toPlayer = _playerTransform.position - transform.position;

            Quaternion toPlayerRotation = Quaternion.LookRotation(toPlayer, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toPlayerRotation, Time.deltaTime * _rotationLerpRate);

            _rigidbody.velocity = transform.forward * _currentSpeed;

            if (toPlayer.magnitude > _spawnRadius * 2)
            {
                transform.position += toPlayer * 1.95f;
            }
        }
    }

    public void Frozen()
    {
        _isFrozen = true;
        _rigidbody.isKinematic = true;
        _animator.speed = 0;
        _skinnedMeshRenderer.material.color = _frozenColor;
    }
    
    public void UnFrozen()
    {
        _isFrozen = false;
        _rigidbody.isKinematic = false;
        _animator.speed = 1;
        _skinnedMeshRenderer.material.color = Color.white;
    }
    
    public void Init(EnemyManager enemyManager, ExperienceManager experienceManager,Transform playerTransform, float spawnRadius)
    {
        _enemyManager = enemyManager;
        _experienceManager = experienceManager;
        _playerTransform = playerTransform;
        _spawnRadius = spawnRadius;
    }

    public void TakeDamage(float damage)
    {
        float newHealth = _currentHealth - damage;
        newHealth = Mathf.Max(newHealth, 0);
        SetHealth(newHealth);
        if (newHealth == 0)
        {
            Die();
        }
    }

    public void Jump(float jumpDuration, float jumpHeight)
    {
        StartCoroutine(JumpRoutine(jumpDuration, jumpHeight));
    }
    
    private IEnumerator JumpRoutine(float jumpDuration, float jumpHeight)
    {
        float timer = 0;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + Vector3.up * jumpHeight;

        while (timer <= jumpDuration)
        {
            float t = timer / jumpDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0;
        while (timer <= jumpDuration * 0.5f)
        {
            float t = timer / jumpDuration * 2f;
            transform.position = Vector3.Lerp(endPosition, startPosition, t);
            timer += Time.deltaTime;
            yield return null;
        }
    }
    
    private IEnumerator FallRoutine(float jumpDuration, float jumpHeight)
    {
        float timer = 0;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + Vector3.up * jumpHeight;

        while (timer <= jumpDuration)
        {
            float t = timer / jumpDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0;
        while (timer <= jumpDuration)
        {
            float t = timer / jumpDuration;
            transform.position = Vector3.Lerp(endPosition, startPosition, t);
            timer += Time.deltaTime;
            yield return null;
        }
    }
    
    private void SetHealth(float value)
    {
        _currentHealth = value;
        _healthBar.SetValueBar(_currentHealth, _fullHealth);
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
        _experienceManager.CreateExperience(transform.position, _expSpawn);
        _enemyManager.RemoveEnemy(this);
        Instantiate(_dyingEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}