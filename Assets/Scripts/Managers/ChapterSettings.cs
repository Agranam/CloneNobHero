using UnityEngine;

[System.Serializable]
public struct EnemyWaves
{
    public Enemy Enemy;
    public float[] NumberPerSecond;
}

[CreateAssetMenu(fileName = "ChapterSettings", menuName = "CloneNobHero/" + "ChapterSettings", order = 0)]
public class ChapterSettings : ScriptableObject
{
    public EnemyWaves[] _enemyWaves;
}