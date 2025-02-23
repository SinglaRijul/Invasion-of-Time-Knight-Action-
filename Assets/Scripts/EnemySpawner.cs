using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    [SerializeField] List<WaveConfigSO> waveConfigs;
    WaveConfigSO currConfig;
    [SerializeField] List<Vector2> gemLocations;

    Vector2 currGemLocation;

    GemController gemController;

    [SerializeField] GameObject bossPrefab;
    [SerializeField] float spawnOffset;
    [SerializeField] float timeBtwWaves = 1f;

    GameManager gameManagerScript;
    int aliveEnemies =0;

    void Awake()
    {
        gemController = FindAnyObjectByType<GemController>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Start()
    {
        StartCoroutine("SpawnEnemies");
        ChangeGemLocation();
    }


    void Update() {
        CheckAliveEnemies();
        //Debug.Log("Alive Enemeis: "+aliveEnemies);
    }

    System.Collections.IEnumerator SpawnEnemies()
    {

        for(int i = 0 ; i < waveConfigs.Count ; i++)
        {
            currConfig = waveConfigs[i];
            for(int j = 0 ; j < currConfig.GetNumbOfEnemies() ; j++)
            {
                Instantiate(currConfig.randomEnemyPrefab() ,
                        GetRandomSpawnPos(),
                        Quaternion.identity,
                        transform);

                yield return new WaitForSeconds(currConfig.GetTimeBetweenSpawn());
            }
            yield return new WaitUntil(() => aliveEnemies == 0);

            ChangeGemLocation();
            yield return new WaitForSeconds(timeBtwWaves);
        }
        

    }



    Vector2 GetRandomSpawnPos()
    {
        float randomX = Random.Range(currGemLocation.x - spawnOffset , currGemLocation.x + spawnOffset);
        float randomY = Random.Range(currGemLocation.y - spawnOffset , currGemLocation.y + spawnOffset);
        
        return new Vector2(randomX , randomY) ;
    }


    void CheckAliveEnemies()
    {
        aliveEnemies = transform.childCount;
    }

    void ChangeGemLocation()
    {
        if(gemLocations.Count>0)
        {
            int randomIdx = Random.Range(0 , gemLocations.Count);
            currGemLocation = gemLocations[randomIdx];
            gemLocations.RemoveAt(randomIdx);

        }
        else{
            currGemLocation = Vector2.zero;

            SpawnBoss();
        }

        gemController.ChangeGemLocation(currGemLocation);

    }

    public Vector2 GetCurrGemLocation()
    {
        return currGemLocation;
    }

    void SpawnBoss()
    {
        Instantiate( bossPrefab,
                new Vector2(4f, 6f),
                Quaternion.identity,
                transform);

        gameManagerScript.PlayBossMusic();

    }
}
