using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////
    [SerializeField] TMP_Text scoreText;
    SceneHandler sceneHandler;
    public const string highScoreKey = "High Score";
    Car car;
    float score;
    int intScore;
    int highScore;
    Canvas canvas;
    RectTransform canvasTransform;
    float canvasTransformX;


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////
    void Start()
    {   
        // Initialize the fields.
        car              = FindObjectOfType<Car>();
        sceneHandler     = FindObjectOfType<SceneHandler>();
        canvas           = FindObjectOfType<Canvas>();
        canvasTransform  = canvas.GetComponent<RectTransform>();
        canvasTransformX = canvasTransform.transform.position.x;

        // If the active scene is "MainMenu", show the high score.
        if(sceneHandler.getCurrentScene() == "MainMenu"){
            highScore = PlayerPrefs.GetInt(ScoreHandler.highScoreKey, 0);
            scoreText.text = $"High Score: {highScore}";
        }

        // Reset the garage pos. 1053
        PlayerPrefs.SetFloat("garagePos", 5000);
    }

    void Update()
    {
        // If the car is not crashed, increase the score over time.
        if(sceneHandler.getCurrentScene() != "MainMenu" && !car.GetIsCrashed()){
            score += Time.deltaTime * car.GetSpeed() * 5;
            intScore = Mathf.FloorToInt(score);
            scoreText.text = "Score: " + intScore.ToString();
        }
    }

    void OnDestroy() {
        // Check and update the high score.
        int currentHighScore = PlayerPrefs.GetInt(highScoreKey, 0);
        if(intScore > currentHighScore){
            PlayerPrefs.SetInt(highScoreKey, intScore);
        }
    }
}
