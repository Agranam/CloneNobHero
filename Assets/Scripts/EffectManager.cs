﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private List<ActiveEffect> _activeEffectsApplied = new();
    [SerializeField] private List<PassiveEffect> _passiveEffectsApplied = new();
    
    [SerializeField] private List<ActiveEffect> _activeEffects = new();
    [SerializeField] private List<PassiveEffect> _passiveEffects = new();

    [SerializeField] private CardManager _cardManager;
    
    private void Awake()
    {
        for (int i = 0; i < _activeEffects.Count; i++)
        {
            _activeEffects[i] = Instantiate(_activeEffects[i]);
        }
        for (int i = 0; i < _passiveEffects.Count; i++)
        {
            _passiveEffects[i] = Instantiate(_passiveEffects[i]);
        }
    }

    private void Start()
    {
        ShowCards();
    }

    [ContextMenu("ShowCards")]
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
        
        _cardManager.ShowCards(effectForCards);
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