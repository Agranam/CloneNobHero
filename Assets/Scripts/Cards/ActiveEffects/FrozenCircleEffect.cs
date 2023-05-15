using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(FrozenCircleEffect), 
    menuName = "CloneNobHero/Effects/ActiveEffect/" + nameof(FrozenCircleEffect), order = 0)]
public class FrozenCircleEffect : ActiveEffect
{
    [Space(5), Header("Frozen Circle")] 
    [SerializeField] private GameObject _frozenEffect;
    [SerializeField] private float _frozenRadius = 3f;
    [SerializeField] private float _frozenTime = 2f;
    [SerializeField] private float _damage = 30;

    private FrozenCircle _frozenCircle;
    
    protected override void Produce()
    {
        Vector3 position = _player.transform.position;
        var go = Instantiate(_frozenEffect, position, Quaternion.identity);
        
        _frozenCircle = go.GetComponent<FrozenCircle>();
        _frozenCircle.SetUp(_frozenRadius);
        _effectsManager.StartCoroutine(UnFroze());
    }

    private IEnumerator UnFroze()
    {
        yield return new WaitForSeconds(_frozenTime);
        
        foreach (var enemy in _frozenCircle.FrozenEnemies)
        {
            enemy.TakeDamage(_damage);
            enemy.UnFrozen();
        }
        
        _frozenCircle.DestroyObj();
    }
}
