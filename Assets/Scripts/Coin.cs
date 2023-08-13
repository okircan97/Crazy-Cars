using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    Transform coinTransform;
    Car car;
    Transform carTransform;
    MapHandler mapHandler;


    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////

    void Start(){
        // Initialize the fields.
        mapHandler    = FindObjectOfType<MapHandler>();
        car           = FindObjectOfType<Car>();
        carTransform  = car.transform;
        coinTransform = transform;
    }

    void Update(){
        // Rotate the coins.
        coinTransform.Rotate(Vector3.up * (20 * Time.deltaTime));
        // Push coins to the pool.
        if(carTransform.position.z >= coinTransform.position.z + 15)
            PushCoinToPool();
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to destroy the coins if they are far enough
    // from the player car.
    void PushCoinToPool(){
        mapHandler.AddCoinToThePool(gameObject);
        Debug.Log("ma");
    }
}
