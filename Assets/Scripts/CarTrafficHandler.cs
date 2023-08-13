using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrafficHandler : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    [SerializeField] GameObject[] carPrefabs;
    GameObject unplayableCar;
    GameObject lastCar;
    GameObject carObject;
    Transform lastCarTransform;
    Transform carTransform;
    ObjPool carPool;
    Car car;
    Vector3 carSpawnPos;


    //////////////////////////////////////
    ///////// START AND UPDATE ///////////
    //////////////////////////////////////
    void Start()
    {
        // Initialize the fields.
        car = FindObjectOfType<Car>();
        carTransform = car.transform;
        // Instantiate the first car wave.
        SpawnCars();
        carPool = new ObjPool();
    }

    void Update()
    {
        // If the position between the last instantiated car and the player car 
        // is small enough, instantiate the new cars.
        if (lastCarTransform)
            if (CheckPosBetween(lastCarTransform.position.z, carTransform.position.z))
            {
                PopFromCarPool();
            }
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to instantiate the cars.
    public void SpawnCars()
    {
        // Two random values will be chosen for each row and the cars will be 
        // instantiated according to those values. The idea is to make the cars
        // instantiate at random positions on a 150x4 matrix.
        // NOTE : ---------------------------------------------------------------
        // The length between cars on the same row is 1.8f, and it's 12f, for the 
        // cars on the same column. Substracting 0.3f from the x axis is because 
        // of some calculation mistakes. 
        // -----------------------------------------------------------------------
        for (int i = 0; i < 150; i++)
        {
            // Get two random values, representing the two j values on the matrix.
            int j1 = Random.Range(0, 4);
            int j2 = Random.Range(0, 4);
            // Randint is a random value to choose a car prefab.
            int randInt = Random.Range(0, carPrefabs.Length);
            // Offset is a random value to make the car positions seem more random.
            float offset = Random.Range(1f, 6f);
            // Instantiation pos of the first car.
            Vector3 spawnPos = new Vector3(j1 * 1.8f - 0.25f,
                                           0,
                                           GetLastCarPosZ() + offset + (i + 1) * 12);
            // Instantiate the first car.
            unplayableCar = Instantiate(carPrefabs[randInt],
                                        spawnPos,
                                        Quaternion.AngleAxis(90, Vector3.up));
            // Instantiate the second car, if the two j values are not the same.
            if (j1 != j2)
            {
                randInt = Random.Range(0, carPrefabs.Length);
                offset = Random.Range(1f, 6f);
                spawnPos = new Vector3(j2 * 1.8f - 0.5f,
                                       0,
                                       GetLastCarPosZ() + offset + (i + 1) * 12);
                unplayableCar = Instantiate(carPrefabs[randInt],
                                            spawnPos,
                                            Quaternion.AngleAxis(90, Vector3.up));
            }
            // Get the last instantiated car. Next cars will be instantiated after it.
            if (i == 149)
            {
                lastCar = unplayableCar;
                lastCarTransform = lastCar.transform;
                Debug.Log("unplayableCar: " + unplayableCar.name);
            }
        }
    }

    // This method is to get the position z of the "lastCar" instantiated.
    public float GetLastCarPosZ()
    {
        if (lastCarTransform)
            return lastCarTransform.position.z;
        else
            return -120f;
    }

    // This method is to check the distance between two values.
    public bool CheckPosBetween(float pos1, float pos2)
    {
        if ((pos1 - pos2) < 120)
        {
            return true;
        }
        else
            return false;
    }

    // Spawning the cars using objects inside the object pool.
    public void PopFromCarPool()
    {
        for (int i = 0; i < 70; i++)
        {
            // Get two random values, representing the two j values on the matrix.
            int j1 = Random.Range(0, 4);
            int j2 = Random.Range(0, 4);
            // Offset is a random value to make the car positions seem more random.
            float offset = Random.Range(1f, 6f);
            // Instantiation pos of the first car.
            carSpawnPos = new Vector3(j1 * 1.8f - 0.3f,
                                           0,
                                           GetLastCarPosZ() + offset + (i + 1) * 12);
            // Pop the first car.
            unplayableCar = carPool.GetFromThePool();
            unplayableCar.transform.position = carSpawnPos;
            // Pop the second car, if the two j values are not the same.
            if (j1 != j2)
            {
                offset = Random.Range(1f, 6f);
                carSpawnPos = new Vector3(j2 * 1.8f - 0.3f,
                                       0,
                                       GetLastCarPosZ() + offset + (i + 1) * 12);
                unplayableCar = carPool.GetFromThePool();
                unplayableCar.transform.position = carSpawnPos;
            }
            // Get the last instantiated car. Next cars will be instantiated after it.
            if (i == 69)
            {
                lastCar = unplayableCar;
                lastCarTransform = lastCar.transform;
            }
        }
    }

    // This method is to add a car object to the pool.
    public void AddCarToThePool(GameObject carObject)
    {
        carPool.AddToThePool(carObject);
    }
}