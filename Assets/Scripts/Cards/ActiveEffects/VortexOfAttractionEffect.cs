using UnityEngine;

[CreateAssetMenu(fileName = nameof(VortexOfAttractionEffect), 
    menuName = "CloneNobHero/Effects/ActiveEffect/" + nameof(VortexOfAttractionEffect), order = 0)]
public class VortexOfAttractionEffect : ActiveEffect
{
    [Space(5), Header("VortexOfAttraction")]
    [SerializeField] private VortexOfAttraction _vortexOfAttraction;
    [SerializeField] private float _pullRadius;
    [SerializeField] private float _pullForce;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _damage;
    [SerializeField] private float _timeToAction = 3f;

    protected override void Produce()
    {
        base.Produce();
        float radius = (float)Random.Range(1, 2f);
        Vector2 randomPos = Random.insideUnitCircle * radius;
        Vector3 spawnPos = _player.transform.position + new Vector3(randomPos.x, 0f, randomPos.y);
        var effect = Instantiate(_vortexOfAttraction, spawnPos, Quaternion.identity);
        effect.SetUp(_pullRadius, _pullForce, _throwForce, _damage, _timeToAction);
    }
}
