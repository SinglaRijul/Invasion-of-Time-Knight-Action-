using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip bossMusic;
    [SerializeField] AudioClip gameOverMusic;
    [SerializeField] AudioClip gameWinMusic;

    [SerializeField] GameObject gameOverObj;
    [SerializeField] GameObject gameWinObj;

    [SerializeField] TextMeshProUGUI timePlayedText_win;
    [SerializeField] TextMeshProUGUI timePlayedText_lose;

    [SerializeField] Slider timeSlider;

    [SerializeField] float startTime=60f;

 

    bool isGameOver = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeSlider.maxValue = 5;
        timeSlider.value = timeSlider.maxValue;
    }


    void Update()
    {


        startTime -= Time.deltaTime;
        timeSlider.value = ((5*startTime)/60f);
        //Debug.Log("time "+ startTime + " " + timeSlider.value);
        if(startTime <0f)
        {
            SetGameOver(true);
        }
        
    }

    public void PlayBossMusic()
    {
        audioSource.clip = bossMusic;
        audioSource.Play();

    }

    public void PlayGameOverMusic()
    {
        audioSource.clip = gameOverMusic;
        audioSource.Play();

    }

    public void PlayGameWinMusic()
    {
        audioSource.clip = gameWinMusic;
        audioSource.Play();

    }


    public void SetGameOver(bool flag)
    {
        isGameOver = flag;
        if(flag) {GameOver();}
    }

    void GameOver()
    {
        Time.timeScale = 0;
        PlayGameOverMusic();
        gameOverObj.SetActive(true);

        timePlayedText_lose.text = "Time Played: " + Time.time + " sec";


    }


    public void GameWin()
    {
        Time.timeScale = 0;
        PlayGameWinMusic();
        gameWinObj.SetActive(true);

        timePlayedText_win.text = "Time Played: " + Time.time + " sec";
    }


    public void OnClickBackToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }


    public void SetStartTime(float value)
    {
        startTime += value;
        timeSlider.value = ((5*startTime)/60f);
    }


}
