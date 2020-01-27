using System.Collections;
using UnityEngine;
using Malee;

public class WaveSpawner : MonoBehaviour
{
    public enum EnemyType { Rat, Skunk };

    [System.Serializable]
    public class Spawn
    {
        public EnemyType enemyType;
        public float wait;
    }

    [Reorderable]
    public SpawnList spawnList;

    [System.Serializable]
    public class SpawnList : ReorderableArray<Spawn> { }

    void Start()
    {
        StartCoroutine(SpawnSequence());
    }

    IEnumerator SpawnSequence()
    {
        int spawnIndex = 0;

        while (spawnIndex < spawnList.Count)
        {
            Spawn spawn = spawnList[spawnIndex];
            SpawnEnemy(spawn.enemyType);
            yield return new WaitForSeconds(spawn.wait);
            spawnIndex++;
        }
    }

    public void SpawnEnemy(string key)
    {
        foreach (EnemyType type in System.Enum.GetValues(typeof(EnemyType)))
        {
            if (key == type.ToString())
            {
                SpawnEnemy(type);
                return;
            }
        }
    }

    public void SpawnEnemy(EnemyType enemyType)
    {
        print(enemyType.ToString());
    }
}
