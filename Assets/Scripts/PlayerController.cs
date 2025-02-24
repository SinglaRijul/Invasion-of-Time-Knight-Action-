using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{



    Vector2 rawInput;

    [SerializeField] float playerXSpeed = 5f;
    [SerializeField] float playerYSpeed = 5f;

    [SerializeField] int playerDamage = 10;
    [SerializeField] AudioClip attackSFX;
    [SerializeField] Animation playerAttackAnim;
    Animator playerAnim;

    AudioSource audioSource;
    Vector2 minBounds;
    Vector2 maxBounds;

    bool isAttacking = false;

    bool isPlayerMoving = false;

    Rigidbody2D playerRb;
    void Start()
    {
        InitBounds();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        MovePlayer();


        AnimatorStateInfo animState = playerAnim.GetCurrentAnimatorStateInfo(0);
        if(animState.IsName("player_attack"))
        {
            isAttacking = true;
        }
        else{
            isAttacking = false;
        }
    }


    void InitBounds()
    {
        minBounds = Camera.main.ViewportToWorldPoint(Vector2.zero);
        maxBounds = Camera.main.ViewportToWorldPoint(Vector2.one);
    }


    void OnMove(InputValue movevalue)
    {
       rawInput = movevalue.Get<Vector2>();
    }


    void MovePlayer()
    {

       // Debug.Log(playerRb.linearVelocity);

        float deltaX = rawInput.x * playerXSpeed * Time.deltaTime ;
        float deltaY = rawInput.y * playerYSpeed  * Time.deltaTime;


        //float newPosx = Mathf.Clamp(transform.position.x + deltaX , minBounds.x , maxBounds.x);
        //float newPosy = Mathf.Clamp(transform.position.y + deltaY , minBounds.y , maxBounds.y);
        transform.position = new Vector3(transform.position.x + deltaX , transform.position.y + deltaY); 

        //Debug.Log(deltaX + " " + deltaY);
        Vector3 playerVelocity =new Vector3(deltaX , deltaY , 1f );

        playerRb.linearVelocity = playerVelocity; 
        //Debug.Log(playerRb.linearVelocity);

       // playerRb.AddForce(Vector2.right * 5f);

        //flip sprite
        isPlayerMoving = Math.Abs(playerRb.linearVelocityX) > Mathf.Epsilon;

        if(isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRb.linearVelocityX)*Math.Abs(transform.localScale.x) , transform.localScale.y);
            
        }


        //run animation
        playerAnim.SetBool("isPlayerMoving", isPlayerMoving);


        // if(rawInput.x>0){
        //     transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) , transform.localScale.y);
        // }
        // else if (rawInput.x <0)
        // {
        //     transform.localScale = new Vector2(-1f*Mathf.Abs(transform.localScale.x) , transform.localScale.y);
        // }
    }

    void OnAttack(InputValue value)
    {
        if(value.isPressed)
        {
            //playerAnim.SetBool("isAttacking", true);
            playerAnim.Play("player_attack");
            
            //play sfx
            audioSource.clip = attackSFX;
            audioSource.Play();
            isAttacking = true;   
            
        }


    }

    // void OnTriggerEnter2D(Collider2D other) 
    // {
    //     if(other.tag == "Enemy" && isAttacking)
    //     {
    //         Debug.Log("Enemy takes damage");
    //         other.gameObject.GetComponent<Enemy>().TakeDamage(10);
    //     }    
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {        
        if(collision.gameObject.tag == "Enemy" && isAttacking)
        {
            Debug.Log("Enemy takes damage");
            collision.gameObject.GetComponent<Enemy>().TakeDamage(playerDamage);
        }   
        
    }


    public void PrepBossFight()
    {
        playerDamage +=20;
        playerXSpeed +=2f;
    }

}
