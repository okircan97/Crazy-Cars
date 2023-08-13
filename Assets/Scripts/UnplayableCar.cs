using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnplayableCar : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    CarTrafficHandler carTrafficHandler;
    Transform unplayableCarTransform;
    Transform carTransform;
    Car car;
    float speed;
    

    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    void Start(){
        car = FindObjectOfType<Car>();
        carTrafficHandler = FindObjectOfType<CarTrafficHandler>();
        speed = 5f;
        unplayableCarTransform = transform;
        carTransform =  car.transform;
    }

    void Update(){
        MoveCar();
        PushToCarPool();
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to move the car.
    void MoveCar(){
        unplayableCarTransform.Translate(Vector3.left * (speed * Time.deltaTime));
    }

    // This method is to destroy the unplayable cars, if they are
    // far enough from the player car.
    void DestroyCar(){
        if(carTransform.position.z >= unplayableCarTransform.position.z + 15)
            Destroy(gameObject);
    }

    // This method is to make the car disappear and add it to the object pool.
    void PushToCarPool(){
        if(carTransform.position.z >= unplayableCarTransform.position.z + 15)
            carTrafficHandler.AddCarToThePool(gameObject);
    }

    // This method is to set the speed value to 0 and to stop the car.
    public void StopCar(){
        speed = 0f;
    }
}
