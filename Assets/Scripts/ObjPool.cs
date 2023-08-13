using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    Stack<GameObject> objectPool = new Stack<GameObject>();
    GameObject obj;

    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // Constructors.
    public ObjPool(){}

    public ObjPool(GameObject obj, int objNum){
        this.obj = obj;
        FillThePool(objNum);
    }

    // This method is to get an object from the pool.
    public GameObject GetFromThePool()
    {
        if(objectPool.Count > 0){
            obj = objectPool.Pop();
            obj.SetActive(true);
            return obj;
        }
        return Object.Instantiate(obj);
    }
    
    // This method is to add an object to the pool.
    public void AddToThePool(GameObject obj){
        obj.gameObject.SetActive(false);
        objectPool.Push(obj);
    }

    // This method is to fill the pool with the given object.
    void FillThePool(int objNum){
        for(int i = 0; i < objNum; i++){
            objectPool.Push(obj);
        }
    }

    // Print stack length.
    public void PrintStackCount(){
        Debug.Log(objectPool.Count);
    }
}
