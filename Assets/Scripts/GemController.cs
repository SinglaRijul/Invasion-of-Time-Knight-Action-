using TMPro;
using UnityEngine;

public class GemController : MonoBehaviour
{
    

    [SerializeField] int gemHealth = 100;

    [SerializeField] AudioClip gemBreakSFX;
    AudioSource audioSource;

    Animator gemAnim;
    [SerializeField] TextMeshProUGUI gemHealthText;
    GameManager gameManagerScript;
    void Start()
    {
        UpdateGemHealthText();
        audioSource = GetComponent<AudioSource>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gemAnim = GetComponent<Animator>();
    }


    void Update()
    {
        
    }

    public void ChangeGemLocation(Vector2 newLocation)
    {
        //make alpha less
        Color gemColor = GetComponent<SpriteRenderer>().color;
        gemColor.a = 0.2f;
        GetComponent<SpriteRenderer>().color = gemColor;

        //move towards new location
        //Debug.Log(newLocation.x-transform.position.x + newLocation.y - transform.position.y);
        transform.Translate(newLocation.x-transform.position.x , newLocation.y - transform.position.y,0);

        //reset alpha
        gemColor.a = 1f;
        GetComponent<SpriteRenderer>().color = gemColor;

    }

    public void TakeDamage(int dmg)
    {
        gemHealth -= dmg;
        UpdateGemHealthText();
        if(gemHealth<=0)
        {
            //PlayDeathAnimation();
            audioSource.clip = gemBreakSFX;
            audioSource.Play();
            gemAnim.SetBool("isAlive" , false);

            var wait = new WaitForSeconds(3f);
            gameManagerScript.SetGameOver(true);
            
            
            Destroy(this.gameObject , 1f);

        }
    }

    void UpdateGemHealthText()
    {
        if(gemHealth<=0){gemHealth=0;}
        gemHealthText.text = "Gem Health: "+ gemHealth;
    }

    public void SetGemHealth(int health)
    {
        gemHealth += health;
        UpdateGemHealthText();
    }
}
