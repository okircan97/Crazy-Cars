using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
public static MusicPlayer musicPlayer;

    // Using the singleton pattern to prevent the 
    // music player from destroying itself between 
    // the scenes.
    private void Awake() {
        if(musicPlayer != null){
            Destroy(gameObject);
        }

        else{
            musicPlayer = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        AudioSource audioSource = GetComponent<AudioSource>();

        if(PlayerPrefs.GetString("MusicPlayer", "ON") == "OFF")
            audioSource.Stop();
    }
}
