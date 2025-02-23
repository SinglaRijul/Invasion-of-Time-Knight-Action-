using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    [SerializeField] List<WaveConfigSO> waveConfigs;
    WaveConfigSO currConfig;
    [
    
    SerializeField] List<Vector2> gemLocations;

    Vector2 currGemLocation;

    [SerializeField] float spawnOffset;
    [SerializeField] float timeBtwWaves = 1f;



    void Start()
    {
        StartCoroutine("SpawnEnemies");
    }

    System.Collections.IEnumerator SpawnEnemies()
    {

        for(int i = 0 ; i < waveConfigs.Count ; i++)
        {
            currConfig = waveConfigs[i];

            Instantiate(currConfig.randomEnemyPrefab() ,
                        GetRandomSpawnPos(),
                        Quaternion.identity,
                        transform);

            yield return new WaitForSeconds(currConfig.GetTimeBetweenSpawn());

        }

        yield return new WaitForSeconds(timeBtwWaves);
    }



    Vector2 GetRandomSpawnPos()
    {
        float randomX = Random.Range(currGemLocation.x - spawnOffset , currGemLocation.x + spawnOffset);
        float randomY = Random.Range(currGemLocation.y - spawnOffset , currGemLocation.y + spawnOffset);
        
        return new Vector2(randomX , randomY) ;
    }
}
