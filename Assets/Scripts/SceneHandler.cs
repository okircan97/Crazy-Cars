using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{   
    public SceneHandler sceneHandler;

    int targetFPS;
    void Start()
    {   
        targetFPS = PlayerPrefs.GetInt("FPS", 60);
        
        // Singleton design to keep it on load.
        if(sceneHandler != null){
            Destroy(gameObject);
        }

        else{
            sceneHandler = this;
            DontDestroyOnLoad(gameObject);
        }
        
        // Set target frame rate.
        Application.targetFrameRate = targetFPS;
    }

    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGarage(){
        SceneManager.LoadScene("Garage");
    }

    public void LoadMarket(){
        SceneManager.LoadScene("Market");
    }

    public void LoadSettings(){
        SceneManager.LoadScene("Settings");
    }

    public void LoadCredits(){
        SceneManager.LoadScene("Credits");
    }

    public void LoadChangeCar(){
        SceneManager.LoadScene("ChangeCar");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public string getCurrentScene(){
        return SceneManager.GetActiveScene().name;
    }

    public int GetTargetFPS(){
        return targetFPS;
    }

    public void SetTargetFPS(){
        if(PlayerPrefs.GetInt("FPS", 60) == 30){
            PlayerPrefs.SetInt("FPS", 60);
            targetFPS = 60;
        }
        else{
            PlayerPrefs.SetInt("FPS", 30);
            targetFPS = 30;
        }

    }
}
