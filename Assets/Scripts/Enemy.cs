
using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

public class Enemy : MonoBehaviour
{



    [SerializeField] Vector2 enemySpeed;
    [SerializeField] int enemyHealth;
    [SerializeField] float deathTime = 3f;
    Vector2 targetPos;

    Animator enemyAnim;
    EnemySpawner enemySpawnerScript;

    Rigidbody2D enemyRb;

    int flipVar =1;

    bool flag = true;

    Vector2 startPos;



    void Awake() {
        enemyAnim = GetComponent<Animator>();
        enemySpawnerScript = GameObject.FindAnyObjectByType<EnemySpawner>();
        enemyRb = GetComponent<Rigidbody2D>();

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

        if(enemySpawnerScript!=null)
        {
            //targetPos = enemySpawnerScript.GetCurrGemLocation();
            targetPos = new Vector2(17f,0f);
        }


        Vector2 randomPos = Random.insideUnitCircle * 2f;
        targetPos -= randomPos;

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
            Destroy(this.gameObject , deathTime);
        }
    }
}
