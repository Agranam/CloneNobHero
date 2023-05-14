using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] private float _delayShowCards = 1f;
    [SerializeField] private List<ActiveEffect> _activeEffectsApplied = new();
    [SerializeField] private List<PassiveEffect> _passiveEffectsApplied = new();
    
    [SerializeField] private List<ActiveEffect> _activeEffects = new();
    [SerializeField] private List<PassiveEffect> _passiveEffects = new();

    [SerializeField] private CardManager _cardManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private Player _player;
    
    private void Awake()
    {
        for (int i = 0; i < _activeEffects.Count; i++)
        {
            _activeEffects[i] = Instantiate(_activeEffects[i]);
            _activeEffects[i].Initialize(this, _enemyManager, _player);
        }
        for (int i = 0; i < _passiveEffects.Count; i++)
        {
            _passiveEffects[i] = Instantiate(_passiveEffects[i]);
            _passiveEffects[i].Initialize(this, _enemyManager, _player);
        }
    }

    private void Update()
    {
        foreach (var activeEffect in _activeEffectsApplied)
        {
            activeEffect.ProcessFrame(Time.deltaTime);
        }
    }

    public void AddEffect(Effect effect)
    {
        if (effect is ActiveEffect activeEffect)
        {
            if (!_activeEffectsApplied.Contains(activeEffect))
            {
                _activeEffectsApplied.Add(activeEffect);
                _activeEffects.Remove(activeEffect);
            }
        } 
        else if (effect is PassiveEffect passiveEffect)
        {
            if (!_passiveEffectsApplied.Contains(passiveEffect))
            {
                _passiveEffectsApplied.Add(passiveEffect);
                _passiveEffects.Remove(passiveEffect);
            }
        }   
        effect.Activate();
    }
    
    public void ShowCards()
    {
        List<Effect> effectsToShow = new();

        foreach (var activeEffect in _activeEffectsApplied)
        {
            if (activeEffect.Level < 10)
                effectsToShow.Add(activeEffect);
        }
        
        foreach (var passiveEffect in _passiveEffectsApplied)
        {
            if (passiveEffect.Level < 10)
                effectsToShow.Add(passiveEffect);
        }
        
        if(_activeEffectsApplied.Count < 4)
            effectsToShow.AddRange(_activeEffects);
        
        if(_passiveEffectsApplied.Count < 4)
            effectsToShow.AddRange(_passiveEffects);

        int numberOfCardsToShow = Mathf.Min(effectsToShow.Count, 3);

        int[] randomIndexes = RandomSort(effectsToShow.Count, numberOfCardsToShow);
        List<Effect> effectForCards = new();
        foreach (var index in randomIndexes)
        {
            effectForCards.Add(effectsToShow[index]);
        }
        
        _cardManager.UpdateCards(effectForCards, _delayShowCards);
    }

    private int[] RandomSort(int lenght, int number)
    {
        int[] array = new int[lenght];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }

        for (int i = 0; i < array.Length; i++)
        {
            int oldValue = array[i];
            int newIndex = Random.Range(0, array.Length);
            array[i] = array[newIndex];
            array[newIndex] = oldValue;
        }

        int[] result = new int[number];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = array[i];
        }

        return result;
    }
}
