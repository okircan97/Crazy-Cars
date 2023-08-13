using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    SceneHandler sceneHandler;
    MusicPlayer musicPlayer;
    AudioSource audioSource;
    [SerializeField] TMP_Text fpsText; 
    [SerializeField] TMP_Text musicText;


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        sceneHandler = FindObjectOfType<SceneHandler>();
        musicPlayer  = FindObjectOfType<MusicPlayer>();
        audioSource = musicPlayer.GetComponent<AudioSource>();
        ArrangeTexts();
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to change the target FPS.
    public void ChangeFPS(){
        sceneHandler.SetTargetFPS();
        fpsText.text = "FPS: " + sceneHandler.GetTargetFPS().ToString();
    }

    // This method is to turn ON/OFF the music.
    public void HandleMusic(){
        if(audioSource.isPlaying){
            audioSource.Stop();
            musicText.text = "Music: OFF";
            PlayerPrefs.SetString("MusicPlayer", "OFF");
        }
        else{
            audioSource.Play();
            musicText.text = "Music: ON";
            PlayerPrefs.SetString("MusicPlayer", "ON");
        }
    }

    // This method is to go back to the main menu.
    public void GoBackToTheMainMenu(){
        sceneHandler.LoadMainMenu();
    }

    // This method is to initialize the TMP_Text values.
    void ArrangeTexts(){
        // Arrange FPS text.
        fpsText.text = "FPS: " + sceneHandler.GetTargetFPS().ToString();
        // Arrange Music text.
        if(audioSource.isPlaying)
            musicText.text = "Music: ON";
        else
            musicText.text = "Music: OFF";
    }
}
