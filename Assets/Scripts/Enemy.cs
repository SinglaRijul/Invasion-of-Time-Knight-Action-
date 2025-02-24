
using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

public class Enemy : MonoBehaviour
{



    [SerializeField] Vector2 enemySpeed;
    [SerializeField] int enemyHealth;
    [SerializeField] float deathTime = 3f;

    [SerializeField] int enemyDamage = 10;
    [SerializeField] bool isTargetPlayer;

    [SerializeField] bool isSupportEnemy;

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

    [SerializeField] float damageCooldown =1f;

    GameManager gameManagerScript;



    void Awake() {

        enemyAnim = GetComponent<Animator>();
        enemySpawnerScript = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gemController = FindAnyObjectByType<GemController>();
        enemyRb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

    }

    void Start()
    {
        startPos = transform.position;

        GetTargetPos();
        StartCoroutine("FlipEnemy");

    }   


    void Update()
    {
        MoveEnemy();



    }


    void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position , targetPos , enemySpeed.x * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(Vector3.up, targetPos);

        if(transform.position == (Vector3)targetPos){

                if(flag)
                {
                    StartCoroutine("StartAttack");
                }

                flag = false;
                PlayAttackingAnimation();
                TakeDamage(0);
            }
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


    void GetTargetPos()
    {
        targetPos = UnityEngine.Vector2.zero ;

        if(!isTargetPlayer)
        {

            if(enemySpawnerScript!=null)
            {
                targetPos = enemySpawnerScript.GetCurrGemLocation();

                //targetPos = new Vector2(17f,0f);
            }


            Vector2 randomPos = Random.insideUnitCircle * 3.5f;
            targetPos -= randomPos;
        }
        else{
            targetPos = playerScript.gameObject.transform.position;

        }
        //Debug.Log(targetPos);

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
        if(!isSupportEnemy)
        {
            while(true)
            {
                gemController.TakeDamage(enemyDamage);
                PlayEnemyAttackSFX();
                yield return new WaitForSeconds(damageCooldown);
            }
        }
        else
        {
            gameManagerScript.SetStartTime(-10f);
        }
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
