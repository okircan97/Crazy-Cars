using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObj : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////
    Car car;
    Transform treeTransform;
    Transform carTransform;

    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////
    void Start()
    {   
        // Initialize the fields.
        car = FindObjectOfType<Car>();
        treeTransform = transform;
        carTransform = car.transform;
    }

    void Update()
    {
        // Move the tree if it's behind the car.
        if(carTransform.position.z >= treeTransform.position.z + 15)
            MoveTree();
    }

    
    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to move the trees forward.
    void MoveTree(){
        treeTransform.position = 
            new Vector3(treeTransform.position.x, 
                        treeTransform.position.y, 
                        treeTransform.position.z + 182);
    }
}
