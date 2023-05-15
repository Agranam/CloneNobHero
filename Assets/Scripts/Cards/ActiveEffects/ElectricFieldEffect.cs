using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ElectricFieldEffect), 
    menuName = "CloneNobHero/Effects/ActiveEffect/" + nameof(ElectricFieldEffect), order = 0)]
public class ElectricFieldEffect : ActiveEffect
{
    [SerializeField] private ElectricField _electricField;
    [SerializeField] private float _timeOfAction;
    [SerializeField] private float _radius = 3;
    [SerializeField] private float _damage = 5;

    private ElectricField _electricFieldInstance; 
    
    protected override void Produce()
    {
        base.Produce();
        Transform playerTransform = _player.transform;

        _electricFieldInstance = Instantiate(_electricField, playerTransform.position, Quaternion.identity);
        _electricFieldInstance.Init(_radius, _damage);
        _electricFieldInstance.transform.SetParent(_player.transform);
        _effectsManager.StartCoroutine(FinishEffect());
    }

    private IEnumerator FinishEffect()
    {
        yield return new WaitForSeconds(_timeOfAction);
        
        _electricFieldInstance.DestroyObj();
    }
}
