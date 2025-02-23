using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Scriptable Objects/WaveConfigSO")]
public class WaveConfigSO : ScriptableObject
{
    
    [SerializeField] List<GameObject> enemyPrefabs;

    [SerializeField] int numbOfEnemies;

    [SerializeField] float baseEnemySpawnTime = 1f;
    [SerializeField] float enemySpawnTimeVariance = 0.2f;
    float timeBtwEnemySpawn;
    

    public GameObject enemyPrefabAt(int idx)
    {
        if(idx>=0 || idx < enemyPrefabs.Count) {return enemyPrefabs[idx];}
        else { return null;}
    }


    public GameObject randomEnemyPrefab()
    {
        return enemyPrefabs[Random.Range( 0 , enemyPrefabs.Count)];
    }

    public float GetTimeBetweenSpawn()
    {
        return Random.Range(baseEnemySpawnTime-enemySpawnTimeVariance , baseEnemySpawnTime+enemySpawnTimeVariance);
    }

    public int GetNumbOfEnemies()
    {
        return numbOfEnemies;
    }


}
