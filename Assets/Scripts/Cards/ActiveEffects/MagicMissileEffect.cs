using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MagicMissileEffect), 
    menuName = "CloneNobHero/Effects/ActiveEffect/" + nameof(MagicMissileEffect), order = 0)]
public class MagicMissileEffect : ActiveEffect
{
    [Space(5),Header("MagicMissileEffect")]
    [SerializeField] private MagicMissile _magicMissile;
    [SerializeField] private float _bulletSpeed = 6f;
    [SerializeField] private float _damage = 30;
    [SerializeField] private int _numberOfBullet = 4;

    protected override void Produce()
    {
        base.Produce();
        _effectsManager.StartCoroutine(EffectProcess());
    }

    private IEnumerator EffectProcess()
    {
        Enemy[] nearestEnemies = _enemyManager.NearestEnemy(_player.transform.position, _numberOfBullet); 

        for (int i = 0; i < nearestEnemies.Length; i++)
        {
            MagicMissile magicMissile = Instantiate(_magicMissile, _player.transform.position, Quaternion.identity);
            magicMissile.SetUp(nearestEnemies[i], _bulletSpeed, _damage);
            yield return new WaitForSeconds(0.2f);
        }    
    }
}