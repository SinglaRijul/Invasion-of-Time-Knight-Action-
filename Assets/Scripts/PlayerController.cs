using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{



    Vector2 rawInput;

    [SerializeField] float playerXSpeed = 5f;
    [SerializeField] float playerYSpeed = 5f;


    Vector2 minBounds;
    Vector2 maxBounds;

    void Start()
    {
        InitBounds();
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
        float deltaX = rawInput.x * playerXSpeed * Time.deltaTime;
        float deltaY = rawInput.y * playerYSpeed * Time.deltaTime;


        float newPosx = Mathf.Clamp(transform.position.x + deltaX , minBounds.x , maxBounds.x);
        float newPosy = Mathf.Clamp(transform.position.y + deltaY , minBounds.y , maxBounds.y);


        transform.position = new Vector3(newPosx , newPosy); 

    }


}
