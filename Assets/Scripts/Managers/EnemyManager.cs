using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private ChapterSettings _chapterSettings;
    [SerializeField] private float _spawnRadius;

    private List<Enemy> _enemiesList = new();

    private void Start()
    {
        StartNewWave(6);
    }

    public void StartNewWave(int wave)
    {
        StopAllCoroutines();
        foreach (var enemyWave in _chapterSettings._enemyWaves)
        {
            if(enemyWave.NumberPerSecond[wave] > 0)
                StartCoroutine(CreateEnemyInSecond(enemyWave.Enemy, enemyWave.NumberPerSecond[wave]));
        }
    }
    
    private IEnumerator CreateEnemyInSecond(Enemy enemy, float enemyPerSecond)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / enemyPerSecond);
            Create(enemy);
        }
    }
    
    public void Create(Enemy enemy)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized;
        Vector3 position = new Vector3(randomPoint.x, 0, randomPoint.y) * _spawnRadius + _playerTransform.position;
        Enemy newEnemy = Instantiate(enemy, position, Quaternion.identity);
        newEnemy.Init(_playerTransform, _spawnRadius);
        _enemiesList.Add(newEnemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemiesList.Remove(enemy);
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(_playerTransform.position, Vector3.up, _spawnRadius);
    }
#endif

}
