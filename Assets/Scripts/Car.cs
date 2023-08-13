using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    string[] carNames    = {"Sport Sedan", "Truck", "SUV", "Tractor", "Delivery Car", "Ambulance", "Firetruck", 
                            "Taxi", "Police Car", "Police Car Alt", "Race Car", "Race Car Alt"};
    float[] startSpeeds  = {10f,  7f,    8f,    6f,     7f,    12f,  12f,  9f,   12f,    10f,    15f,  15f};
    float[] maxSpeeds    = {33f,  25f,   30f,   20f,    25f,   30f,  28f,  33f,  35f,    27f,    40f,  40f};
    float[] carHandlings = {80f,  60f,   80f,   50f,    60f,   90f,  85f,  70f,  80f,    65f,    100f, 100f};
    float[] carAccs      = {0.3f, 0.175f, 0.25f, 0.125f, 0.15f, 0.4f, 0.4f, 0.3f, 0.275f, 0.275f, 0.6f, 0.6f};
    [SerializeField]  float speed;
    [SerializeField]  float maxSpeed;
    [SerializeField]  float acc;
    [SerializeField]  float steerSpeed;
    [SerializeField]  int steerPos;
    [SerializeField]  AudioSource coinAudioSource;
    [SerializeField]  AudioSource crashAudioSource;
    SceneHandler      sceneHandler;
    GoldHandler       goldHandler;
    MapHandler        mapHandler;
    Transform         carTransform;
    bool              isCrashed;      


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    void Start()
    {   

        // Get the currentCar playerpref. If can not find it, 
        // update it as "Sport Sedan".
        string currentCarStr = PlayerPrefs.GetString(Garage.currentCarKey, 
                                                     Garage.sportSedanKey);

        // Get the index of the current car.
        for(int i = 0; i < carNames.Length; i++){
            if(carNames[i] == currentCarStr){
                // Initialize the car details.
                speed        = startSpeeds[i];
                maxSpeed     = maxSpeeds[i];
                acc          = carAccs[i];
                steerSpeed   = carHandlings[i];
                break;
            }
        }

        // Initialize the other fields.
        carTransform     = transform;
        steerPos          = 0;
        isCrashed         = false;
        goldHandler       = FindObjectOfType<GoldHandler>();
        mapHandler        = FindObjectOfType<MapHandler>();
        sceneHandler      = FindObjectOfType<SceneHandler>();

        // Activate the prefab attached to the gameobject with the name of 
        // the currentCarStr.
        gameObject.transform.Find(currentCarStr).gameObject.SetActive(true);
    }

    void FixedUpdate(){   
        // Move the car.
        MoveCar();

        // If the car is crashed, stop the car.
        if(isCrashed){
            StopCar();
        }
    }


    // //////////////////////////////////////
    // //////////// COLLUSIONS //////////////
    // //////////////////////////////////////

    void OnTriggerEnter(Collider other) 
    {
        // On collusion with obstacles and unplayable cars, load the 
        // main menu.
        // if(other.CompareTag("Obstacle")){
        if(other.CompareTag("Obstacle") || other.CompareTag("Unplayable Car")){
            Debug.Log(other.name);
            isCrashed = true;
            crashAudioSource.Play();
            // If collided with a car, stop the other car.
            if(other.tag == "Unplayable Car"){
                other.GetComponent<UnplayableCar>().StopCar();
            }
            StartCoroutine(WaitAndLoad(1.5f));
        }

        // On collusion with coins, increase the total gold.
        else if(other.CompareTag("Coin")){
            mapHandler.AddCoinToThePool(other.gameObject);
            // other.gameObject.SetActive(false);
            goldHandler.increaseGold();
            coinAudioSource.Play();
        }

        // On collusion with the map spawn point, spawn the new map,
        // and pop the coins.
        else if(other.CompareTag("Map Spawn Point")){
            mapHandler.SpawnMap();
            mapHandler.PopCoinFromThePool();
        }

        // On collusion with the map destroy point, destroy the previous map
        // and calculate the time passed.
        else if(other.CompareTag("Map Destroy Point")){
            GameObject toDestroy = other.transform.parent.gameObject;
            StartCoroutine(WaitAndDestroy(1.5f, toDestroy));
        }
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // A coroutine to load a scene.
    public IEnumerator WaitAndLoad(float awaitTime){
        yield return new WaitForSeconds(awaitTime);
        sceneHandler.LoadMainMenu();
    }

    // A coroutine to destroy a game object.
    public IEnumerator WaitAndDestroy(float awaitTime, GameObject gameObject){
        yield return new WaitForSeconds(awaitTime);
        Destroy(gameObject);
    }

    // This method is to move the car with a constant acceleration.
    void MoveCar(){
        // Using extra pharanthases is good for performance.
        carTransform.Rotate(Vector3.up * (steerSpeed * steerPos * Time.deltaTime));
        carTransform.Translate(Vector3.forward * (speed * Time.deltaTime));
        speed += acc * Time.deltaTime;
        // If the speed is higher than max speed, stop the acceleration.
        if(speed >= maxSpeed){
            acc = 0;
        }
        // When steering is stop, turn the car to Quaternion.identity over time.
        if(steerPos == 0){
            carTransform.rotation = Quaternion.Lerp(carTransform.rotation, 
                                                    Quaternion.identity, 
                                                    Time.deltaTime*10);
        }
    }

    // This method is to stop the car and the acceleration.
    void StopCar(){
        speed = 0f;
        acc = 0f;
        // Crash effect.
        carTransform.Translate(Vector3.back * 0.025f * 10 * Time.deltaTime);
    }

    // This method is to change the steering position.
    public void Steer(int direction){
        steerPos = direction;
    }

    // Getter method for isCrashed.
    public bool GetIsCrashed(){
        return isCrashed;
    }

    // Getter method for the speed.
    public float GetSpeed(){
        return speed;
    }
}
