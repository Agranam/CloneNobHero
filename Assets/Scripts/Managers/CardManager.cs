using System;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardManagerParent;
    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private Card[] _effectCards;

    private void Awake()
    {
        foreach (var effectCard in _effectCards)
        {
            effectCard.Init(_effectsManager, this);
        }
    }

    public void UpdateCards(List<Effect> effects, float delay)
    {
        _gameStateManager.SetCardState();
        
        for (int i = 0; i < effects.Count; i++)
        {
            _effectCards[i].Show(effects[i]);
        }

        ShowCards();
        //Invoke(nameof(ShowCards), delay);
    }

    private void ShowCards()
    {
        _cardManagerParent.SetActive(true);
    }

    public void HideCards()
    {
        _cardManagerParent.SetActive(false);
        
        _gameStateManager.SetAction();
    }
}
