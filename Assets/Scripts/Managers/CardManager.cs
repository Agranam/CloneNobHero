using System;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardManagerParent;
    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private Card[] _effectCards;

    private void Awake()
    {
        foreach (var effectCard in _effectCards)
        {
            effectCard.Init(_effectsManager, this);
        }
    }

    public void ShowCards(List<Effect> effects)
    {
        _cardManagerParent.SetActive(true);
        for (int i = 0; i < effects.Count; i++)
        {
            _effectCards[i].Show(effects[i]);
        }
    }

    public void HideCards()
    {
        _cardManagerParent.SetActive(false);
    }
}
