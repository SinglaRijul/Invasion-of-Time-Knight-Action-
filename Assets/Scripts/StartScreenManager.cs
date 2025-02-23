using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartScreenManager : MonoBehaviour
{

    [SerializeField] GameObject settingsObj;
    [SerializeField] string gameSceneText = "GameScene";


    [SerializeField] List<Button> menuButtons;
    

    void Start()
    {
        
    }
    void Update()
    {
        
    }


    public void OnClickStart()
    {
        SceneManager.LoadScene(gameSceneText);
    }

    public void OnClickQuit()
    {

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Applciation.Quit();
        #endif

    }

    public void OnClickSettings()
    {
        settingsObj.SetActive(!settingsObj.activeInHierarchy);
        DisableButtons();
    }

    public void OnPressBackButton()
    {
        settingsObj.SetActive(!settingsObj.activeInHierarchy);
        
        EnableButtons();
    }

    void DisableButtons()
    {
        for(int i = 0 ;i < menuButtons.Count ; i++)
        {
            menuButtons[i].interactable = false;
        }
    }

    void EnableButtons()
    {
        for(int i = 0 ;i < menuButtons.Count ; i++)
        {
            menuButtons[i].interactable = true;
        }
    }
}
