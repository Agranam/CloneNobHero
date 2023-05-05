using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _iconBackground;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Button _button;

    [SerializeField] private Sprite _activeEffectSprite;
    [SerializeField] private Sprite _passiveEffectSprite;

    private Effect _effect;
    
    public void Show(Effect effect)
    {
        _effect = effect;

        _nameText.text = effect.Name;
        _descriptionText.text = effect.Description;
        _levelText.text = effect.Level.ToString();
        _iconImage.sprite = effect.Sprite;
            
        if (effect is ActiveEffect)
        {
            _iconBackground.sprite = _activeEffectSprite;
        }
        else if (effect is PassiveEffect)
        {
            _iconBackground.sprite = _passiveEffectSprite;

        }
    }
}
