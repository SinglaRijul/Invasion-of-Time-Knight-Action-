
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BossScript : MonoBehaviour
{



    [SerializeField] Vector2 enemySpeed;
    [SerializeField] int enemyHealth;
    [SerializeField] float deathTime = 3f;

    [SerializeField] int enemyDamage = 50;
    
    [SerializeField] List<GameObject> enemyPrefabs;

    [SerializeField] AudioClip attackSFX;
    [SerializeField] AudioClip deathSFX;
    Vector2 targetPos;

    Animator enemyAnim;
    EnemySpawner enemySpawnerScript;

    PlayerController playerScript;

    GemController gemController;

    Rigidbody2D enemyRb;
    AudioSource audioSource;

    int flipVar =1;

    bool flag = true;

    Vector2 startPos;

    [SerializeField] float damageCooldown = 0.3f;



    void Awake() {

        enemyAnim = GetComponent<Animator>();
        enemySpawnerScript = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gemController = FindAnyObjectByType<GemController>();
        enemyRb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

    }

    void Start()
    {
        startPos = transform.position;
        StartCoroutine("FlipEnemy");
        StartCoroutine("StartAttack");


    }   


    void Update()
    {
        MoveEnemy();



    }


    void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position , new Vector2(Random.Range(-4f,4f), Random.Range(-6f,6f)), enemySpeed.x * Time.deltaTime);
        

        // if(transform.position == (Vector3)targetPos){

        //         if(flag)
        //         {
        //             StartCoroutine("StartAttack");
        //         }

        //         flag = false;
        //         PlayAttackingAnimation();
        //         TakeDamage(0);
        //     }
    }

    IEnumerator FlipEnemy()
    {

        while(flag)
        {
            transform.localScale = new Vector2(flipVar * Mathf.Abs(transform.localScale.x) , transform.localScale.y);
            //Debug.Log("here");
            flipVar *= -1;
            yield return new WaitForSeconds(0.5f);
        }
    }



    void PlayAttackingAnimation()
    {
        enemyAnim.SetBool("isAttacking" , true);

    }


    void PlayDeathAnimation()
    {
        enemyAnim.SetBool("isAlive" , false);
    }


    public void TakeDamage(int dmg)
    {
        enemyHealth -= dmg;
        if(enemyHealth<=0)
        {
            PlayDeathAnimation();
            PlayEnemyDeathSFX();
            Destroy(this.gameObject , deathTime);
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(2f);

        PlayAttackingAnimation();

        do{
            //gemController.TakeDamage(enemyDamage);
            //PlayEnemyAttackSFX();

            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)],
                        new Vector2(Random.Range(-4f,4f), Random.Range(-6f,6f)),
                        Quaternion.identity,
                        transform);

            yield return new WaitForSeconds(damageCooldown);
        }while(true);
        
    }


    void PlayEnemyAttackSFX()
    {
        audioSource.clip = attackSFX;
        audioSource.Play();
    }

    void PlayEnemyDeathSFX()
    {
        audioSource.clip = deathSFX;
        audioSource.Play();

    }
}
