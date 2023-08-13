using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldHandler : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    [SerializeField] TMP_Text goldText;
    int currentGold;
    public const string goldKey = "Golds";
    Car car;

    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    void Start()
    {
        currentGold = 0;
        car = FindObjectOfType<Car>();
    }

    void Update()
    {
        // Get the gold current amount.
        goldText.text = "Gold: " + currentGold.ToString();
    }

    // When the game object destroys, update the player prefs' goldKey
    // and the highScoreKey.
    void OnDestroy() {
        // Increase the gold amount.
        int maxGold = PlayerPrefs.GetInt(goldKey, 0);
        maxGold = maxGold + currentGold;
        PlayerPrefs.SetInt(goldKey, maxGold);
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // Getter method for the gold.
    // public int getGold(){
    //     return currentGold;
    // }

    // A method to increase the gold amount.
    public void increaseGold(){
        currentGold += 10;
    }
}
