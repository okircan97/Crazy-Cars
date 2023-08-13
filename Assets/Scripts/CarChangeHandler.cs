using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarChangeHandler : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////
    SceneHandler sceneHandler;
    [SerializeField] TMP_Text carText;
    [SerializeField] TMP_Text carSpeedText;
    [SerializeField] TMP_Text carHandlingText;
    [SerializeField] TMP_Text carAccText;
    string carToChange;
    string[] carNames     = {"Sport Sedan", "Truck", "SUV", "Tractor","Delivery Car", "Ambulance", "Firetruck", 
                             "Taxi","Police Car", "Police Car Alt", "Race Car", "Race Car Alt"};
    float[]  maxSpeeds    = {33f,  25f,   30f,   20f,    25f,   30f,  28f,  33f,  35f,    27f,    40f,  40f};
    float[]  carHandlings = {80f,  60f,   80f,   50f,    60f,   90f,  85f,  70f,  80f,    65f,    100f, 100f};
    float[]  carAccs      = {0.3f, 0.175f, 0.25f, 0.125f, 0.15f, 0.4f, 0.4f, 0.3f, 0.275f, 0.275f, 0.6f, 0.6f};
    float startSpeed;
    float maxSpeed;
    float carHandling;
    float carAcc;


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    void Start()
    {
        // Initialize the fields.
        sceneHandler = FindObjectOfType<SceneHandler>();

        // Get which car the player'd like to buy.
        carToChange = PlayerPrefs.GetString(Garage.carToChangeKey, "0");
        Debug.Log("Car name: " + carToChange);

        // Update the car text.
        carText.text = carToChange;

        // Find the "carToBuy" on the list carNames and update
        // the car details according to its index number.
        for(int i = 0; i < carNames.Length; i++){
            if(carNames[i] == carToChange){
                maxSpeed             = maxSpeeds[i];
                carHandling          = carHandlings[i];
                carAcc               = carAccs[i];
                carSpeedText.text    = "Speed:            " + (maxSpeed * 5).ToString();
                carHandlingText.text = "Handling:       " + carHandling.ToString();
                carAccText.text      = "Acceleration: " + carAcc.ToString();
                break;
            }
        }
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to change the current car.
    public void ChangeCar(){
        PlayerPrefs.SetString(Garage.currentCarKey, carToChange);
        sceneHandler.LoadGarage();
    }
}
