using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    GameObject cars;       
    bool moveThem;         // Bool to start/stop moving the menu UI.
    int timesMoved = 1;    // Number of times the menu UI is moved.


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////
    void Start()
    {
        cars = GameObject.FindGameObjectWithTag("Cars");
    }

    void Update()
    {
        if(moveThem){
            moveButtonAndTexts();
            if(timesMoved == 0){
                if(cars.transform.position.x >= 540){
                    cars.transform.position = new Vector3(540,1170,0);
                    moveThem = false;
                    timesMoved++;
                }
            }
            else{
                if(cars.transform.position.x <= -1094 * timesMoved + 540){
                    cars.transform.position = new Vector3(540 -1094*timesMoved, 1170,0);
                    moveThem = false;
                    timesMoved++;
                    if(timesMoved > 2){
                        timesMoved = 0;
                    }
                }
            }
        }
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to move the car buttons and texts.
    public void moveButtonAndTexts(){
        moveThem = true;
        if(timesMoved == 0){
            cars.transform.Translate(Vector3.right * Time.deltaTime * 1500);
        }
        else{
            cars.transform.Translate(Vector3.left * Time.deltaTime * 1000);
        }
    }
}
