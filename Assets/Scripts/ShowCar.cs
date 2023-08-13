using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCar : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    [SerializeField] GameObject[] showCars;
    SceneHandler sceneHandler;
    string currentCarStr;


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    void Start()
    {
        sceneHandler = FindObjectOfType<SceneHandler>();
        
        // If the active scene is the market, get the carToBuyKey.
        if(sceneHandler.getCurrentScene() == "Market"){
            currentCarStr = PlayerPrefs.GetString(Garage.carToBuyKey, Garage.sportSedanKey);
            
        }

        // If the active scene is the "ChangeCar", get the carToChangeKey.
        else if(sceneHandler.getCurrentScene() == "ChangeCar"){
            currentCarStr = PlayerPrefs.GetString(Garage.carToChangeKey, Garage.sportSedanKey);
        }

        // If not, get the current car.
        else{
            currentCarStr = PlayerPrefs.GetString(Garage.currentCarKey, Garage.sportSedanKey);
        }

        // Instantiate the car with the right name. 
        for(int i = 0; i < showCars.Length; i++){
            if(showCars[i].name == currentCarStr){
                Instantiate(showCars[i], transform.position, Quaternion.identity);
            }
        }
    }
}
