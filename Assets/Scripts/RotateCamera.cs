using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // This class is to rotate the camera around a given object. 
    [SerializeField] GameObject target;
    
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime * 20);
    }
}
