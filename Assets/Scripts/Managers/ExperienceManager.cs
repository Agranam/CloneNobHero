using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public static UnityEvent LevelUp = new ();

    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _experienceBar;
    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private AnimationCurve _experienceCurve;

    [SerializeField] private float _experience;
    [SerializeField] private float _nextLevelExperience;
    private int _level;

    private void Awake()
    {
        GetNextLevelExperience();
    }

    private void GetNextLevelExperience()
    {
        _nextLevelExperience = (int)_experienceCurve.Evaluate(_level);
    }

    public void AddExperience(int value)
    {
        _experience += value;
        DisplayExperience();
        if (_experience >= _nextLevelExperience)
        {   
            UpLevel();
        }
    }

    public void UpLevel()
    {
        _level++;
        _levelText.text = _level.ToString("00");
        _experience = 0;
        _effectsManager.ShowCards();
        GetNextLevelExperience();
        DisplayExperience();
        LevelUp.Invoke();
    }

    private void DisplayExperience()
    {
        _experienceBar.fillAmount = _experience / _nextLevelExperience;
    }
}
