using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Garage : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////
    [SerializeField] TMP_Text maxGoldText;
    int maxGold;
    public const string sportSedanKey = "Sport Sedan";
    public const string currentCarKey = "Current Car";
    public const string carToBuyKey   = "CarToBuy";
    public const string carToChangeKey   = "CarToChange";

    Color color1 = new Color(255f/255f,255f/255f,255f/255f,255f/255f);
    Color color2 = new Color(255f/255f,255f/255f,255f/255f,75f/255f);
    SceneHandler sceneHandler;
    GameObject carScroll;
    Vector3 oldTransform;
    GameObject[] pageNumbers;
    GameObject pageNumber1;
    GameObject pageNumber2;
    GameObject pageNumber3;
    Canvas canvas;
    RectTransform canvasTransform;
    float canvasTransformX;


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {   
        // Initialize the fields.
        sceneHandler     = FindObjectOfType<SceneHandler>();
        carScroll        = GameObject.FindGameObjectWithTag("Car Scroll");
        oldTransform     = carScroll.transform.position;
        canvas           = FindObjectOfType<Canvas>();
        canvasTransform  = canvas.GetComponent<RectTransform>();
        canvasTransformX = canvasTransform.transform.position.x;
        pageNumbers  = GameObject.FindGameObjectsWithTag("Page Number");
        pageNumber1  = Array.Find(pageNumbers, 
                                  pageNumber => pageNumber.name == "PageNumber1");
        pageNumber2  = Array.Find(pageNumbers, 
                                  pageNumber => pageNumber.name == "PageNumber2");
        pageNumber3  = Array.Find(pageNumbers, 
                                  pageNumber => pageNumber.name == "PageNumber3");

        // Test
        // ------------------------------------------------
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.SetInt(GoldHandler.goldKey, 500000);
        // PlayerPrefs.SetString(taxiKey, "0");
        // PlayerPrefs.SetString(policeKey, "0");
        // ------------------------------------------------

        // The sport sedan is bought by default.
        PlayerPrefs.SetString(sportSedanKey, "1");

        // Get the current car key. If it is empty, change 
        // it with sportSedanKey.
        PlayerPrefs.GetString(currentCarKey, sportSedanKey);

        // Get the gold amount and show it on the maxGoldText.
        maxGold = PlayerPrefs.GetInt(GoldHandler.goldKey, 0);
        maxGoldText.text = String.Format("{0:#,0}", maxGold);

        // Check which cars are bought and update their images' color.
        GameObject[] carButtons =  GameObject.FindGameObjectsWithTag("Car Button");
        for(int i = 0; i < carButtons.Length; i++){
            if(PlayerPrefs.GetString(carButtons[i].name, "0") == "1"){
                GameObject carImage = 
                           carButtons[i].transform.Find(carButtons[i].name + " Img").gameObject;
                carImage.GetComponent<RawImage>().color = color1;
            }
        }

        // Move the carScroll pos where it's left of when reloading the page.
        float xPos = PlayerPrefs.GetFloat("garagePos", carScroll.transform.position.x);
        carScroll.transform.position = new Vector3(xPos, 
                                                    carScroll.transform.position.y, 
                                                    carScroll.transform.position.z);
        

        // Update the "PageNumber" colors.
        ArrangePageNumberColor();
    }

    void Update(){
        // If the carScroll's pos is changed, update the 
        // color of the page number dots.
        if(carScroll.transform.position != oldTransform){
            ArrangePageNumberColor();
            oldTransform = carScroll.transform.position;
        }
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to change the current car.
    public void ChangeCar(int price){
        // Get the car's name on the clicked button.
        string carName = EventSystem.current.currentSelectedGameObject.name;

        // Check if the car is bought or not. "1" is for bought, "0" is for not.
        string carState = PlayerPrefs.GetString(carName, "0");

        // If the car is bought, load the changeCarScene.
        if(carState == "1"){
            // Save the pos of the carScroll.
            PlayerPrefs.SetFloat("garagePos", carScroll.transform.position.x);
            // Get which car you'd like to drive.
            PlayerPrefs.SetString(carToChangeKey, carName);
            sceneHandler.LoadChangeCar();

            // PlayerPrefs.SetString(currentCarKey, carName);
            // sceneHandler.LoadGarage();

        }
        // If not, update carToBuyKey and load the market scene.
        else{
            PlayerPrefs.SetFloat("garagePos", carScroll.transform.position.x);
            PlayerPrefs.SetString(carToBuyKey, carName);
            sceneHandler.LoadMarket();
        }
    }

    // This method is to check the position of the "carScroll" and change
    // the color of the "Page Number" objects accordingly.
    void ArrangePageNumberColor(){

        if(carScroll.transform.position.x >= 300 + canvasTransformX){
            pageNumber1.GetComponent<RawImage>().color = color1;
            pageNumber2.GetComponent<RawImage>().color = color2;
            pageNumber3.GetComponent<RawImage>().color = color2;
        }

        else if(carScroll.transform.position.x <= -300 + canvasTransformX){
            pageNumber1.GetComponent<RawImage>().color = color2;
            pageNumber2.GetComponent<RawImage>().color = color2;
            pageNumber3.GetComponent<RawImage>().color = color1;
        }

        else{
            pageNumber1.GetComponent<RawImage>().color = color2;
            pageNumber2.GetComponent<RawImage>().color = color1;
            pageNumber3.GetComponent<RawImage>().color = color2;
        }

    }
}
