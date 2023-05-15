using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ExperienceManager : MonoBehaviour
{
    public static UnityEvent LevelUp = new ();

    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _experienceBar;
    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private ExperienceLoot _expPrefab;
    [SerializeField] private AnimationCurve _experienceCurve;
    
    [SerializeField] private float _experience;
    [SerializeField] private float _nextLevelExperience;
    
    private int _level = -1;

    private void Awake()
    {
        GetNextLevelExperience();
    }

    private void GetNextLevelExperience()
    {
        _nextLevelExperience = (int)_experienceCurve.Evaluate(_level);
    }

    public void AddExperience(float value)
    {
        _experience += value;
        DisplayExperience();
        if (_experience >= _nextLevelExperience)
        {   
            UpLevel();
        }
    }

    public void CreateExperience(Vector3 position, float expValue)
    {
        float radius = (float)Random.Range(1, 2f);
        Vector2 randomPos = Random.insideUnitCircle * radius;
        Vector3 spawnPos = position + new Vector3(randomPos.x, 0f, randomPos.y);
        var exp = Instantiate(_expPrefab, position, Quaternion.identity);
        exp.SetExperienceValue(expValue);
        exp.Fall(spawnPos);
    }
    
    public void UpLevel()
    {
        _level++;
        _levelText.text = _level.ToString("00");
        _experience = 0;
        _enemyManager.StartNewWave(_level);
        _effectsManager.ShowCards();
        GetNextLevelExperience();
        DisplayExperience();
        _playerMove.StopMove();
        LevelUp.Invoke();
    }

    private void DisplayExperience()
    {
        _experienceBar.fillAmount = _experience / _nextLevelExperience;
    }
}
