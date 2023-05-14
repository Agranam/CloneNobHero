using UnityEngine;

[CreateAssetMenu(fileName = nameof(ShadowMissilesEffect), 
    menuName = "CloneNobHero/Effects/ActiveEffect/" + nameof(ShadowMissilesEffect), order = 0)]
public class ShadowMissilesEffect : ActiveEffect
{
    [Space(5),Header("ShadowMissilesEffect")]
    [SerializeField] private ShadowMissile _shadowMissile;
    [SerializeField] private float _bulletSpeed = 6f;
    [SerializeField] private float _damage = 30;

    private int _numberOfMissiles = 5;
    
    protected override void Produce()
    {
        base.Produce();
        Transform playerTransform = _player.transform;

        for (int i = 0; i < _numberOfMissiles; i++)
        {
            float angle = (360f / _numberOfMissiles) * i;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * playerTransform.forward;
            ShadowMissile newBullet = Instantiate(_shadowMissile, playerTransform.position, Quaternion.identity);
            newBullet.SetUp(direction * _bulletSpeed, _damage, 2);
        }
    }
}