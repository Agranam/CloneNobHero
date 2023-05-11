using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea(1,3)]
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _level = -1;

    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public int Level => _level;

    public virtual void Activate()
    {
        _level++;
    }
}