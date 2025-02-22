using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{



    Vector2 rawInput;

    [SerializeField] float playerXSpeed = 5f;
    [SerializeField] float playerYSpeed = 5f;

    Animator playerAnim;
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
    }


    void Update()
    {
        MovePlayer();
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
        float deltaX = rawInput.x * playerXSpeed * Time.deltaTime ;
        float deltaY = rawInput.y * playerYSpeed  * Time.deltaTime;


        //float newPosx = Mathf.Clamp(transform.position.x + deltaX , minBounds.x , maxBounds.x);
        //float newPosy = Mathf.Clamp(transform.position.y + deltaY , minBounds.y , maxBounds.y);
        transform.position = new Vector3(transform.position.x + deltaX , transform.position.y + deltaY); 

        
        //playerRb.linearVelocity = new Vector3(deltaX , deltaY ,0f);
        //Debug.Log(playerRb.linearVelocity);

        //flip sprite
        isPlayerMoving = Math.Abs(playerRb.linearVelocityX) > Mathf.Epsilon;

        if(isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRb.linearVelocityX)*transform.localScale.x , transform.localScale.y);
        }

    }

    void OnAttack(InputValue value)
    {
        if(value.isPressed)
        {
            playerAnim.SetBool("isAttacking", !isAttacking);
            isAttacking = !isAttacking;   
        }

    }

}
