using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////
    [SerializeField] TMP_Text maxGoldText;
    int maxGold;
    SceneHandler sceneHandler;
    [SerializeField] TMP_Text carText;
    [SerializeField] TMP_Text carPriceText;
    [SerializeField] TMP_Text carSpeedText;
    [SerializeField] TMP_Text carHandlingText;
    [SerializeField] TMP_Text carAccText;
    [SerializeField] Button buyButton;
    string carToBuy;
    string[] carNames     = {"Sport Sedan", "Truck", "SUV", "Tractor","Delivery Car", "Ambulance", "Firetruck", 
                             "Taxi","Police Car", "Police Car Alt", "Race Car", "Race Car Alt"};
    int[]    carPrices    = {0, 10000, 15000, 20000, 25000, 30000, 30000, 40000, 50000, 55000, 150000, 150000};
    float[]  maxSpeeds    = {33f,  25f,   30f,   20f,    25f,   30f,  28f,  33f,  35f,    27f,    40f,  40f};
    float[]  carHandlings = {80f,  60f,   80f,   50f,    60f,   90f,  85f,  70f,  80f,    65f,    100f, 100f};
    float[]  carAccs      = {0.3f, 0.175f, 0.25f, 0.125f, 0.15f, 0.4f, 0.4f, 0.3f, 0.275f, 0.275f, 0.6f, 0.6f};
    int carPrice;
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

        // Get the gold amount and show it on the maxGoldText.
        maxGold = PlayerPrefs.GetInt(GoldHandler.goldKey, 0);
        maxGoldText.text = String.Format("{0:#,0}", maxGold);

        // Get which car the player'd like to buy.
        carToBuy = PlayerPrefs.GetString(Garage.carToBuyKey, "0");
        Debug.Log("Car name: " + carToBuy);

        // Update the car text.
        carText.text = carToBuy;

        // Find the "carToBuy" on the list carNames and update
        // the car details according to its index number.
        for(int i = 0; i < carNames.Length; i++){
            if(carNames[i] == carToBuy){
                carPrice             = carPrices[i];
                maxSpeed             = maxSpeeds[i];
                carHandling          = carHandlings[i];
                carAcc               = carAccs[i];
                carPriceText.text    = String.Format("{0:#,0}", carPrices[i]);
                carSpeedText.text    = "Speed:            " + (maxSpeed * 5).ToString();
                carHandlingText.text = "Handling:       " + carHandling.ToString();
                carAccText.text      = "Acceleration: " + carAcc.ToString();
                break;
            }
        }

        // If the car's price is higher than the gold amount, 
        // then lock the buy button.
        if(carPrice > maxGold){
            buyButton.interactable = false;
        }
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to buy a car.
    public void BuyCar(){
        if(carPrice <= maxGold){
            Debug.Log($"You just bought a {carToBuy}");
            // Update the car on the playerprefs as bought.
            PlayerPrefs.SetString(carToBuy, "1");
            // Update the maxGold value and the gold text.
            Debug.Log("Before gold: "+ maxGold);
            Debug.Log("Price: " + carPrice);
            maxGold -= carPrice;
            Debug.Log("After gold: " + maxGold);
            PlayerPrefs.SetInt(GoldHandler.goldKey, maxGold);
            maxGoldText.text = $"Gold: {maxGold}";
            // Load back the garage.
            sceneHandler.LoadGarage();
        }
        else{
            Debug.Log("You don't have enough money.");
        }
    }
}
