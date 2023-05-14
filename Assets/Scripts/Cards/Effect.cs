using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea(1,3)]
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _level = -1;

    protected EffectsManager _effectsManager;
    protected EnemyManager _enemyManager;
    protected Player _player;
    
    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public int Level => _level;

    public virtual void Initialize(EffectsManager effectsManager, EnemyManager enemyManager, Player player)
    {
        _effectsManager = effectsManager;
        _enemyManager = enemyManager;
        _player = player;
    }
    
    public virtual void Activate()
    {
        _level++;
    }
}