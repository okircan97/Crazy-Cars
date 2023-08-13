using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    // //////////////////////////////////////
    // ////////////// FIELDS ////////////////
    // //////////////////////////////////////

    [SerializeField] GameObject map;
    [SerializeField] GameObject trees;
    [SerializeField] GameObject coinPrefab;
    Vector3 mapSpawnPos;
    Vector3 treeSpawnPos;
    Vector3 coinSpawnPos;
    int spawnedMapNumber = 0;
    ObjPool coinPool;
    GameObject coin;
    float offset;

    // //////////////////////////////////////
    // ///////// START AND UPDATE ///////////
    // //////////////////////////////////////
    
    void Start()
    {   
        coinPool = new ObjPool(coinPrefab, 0);
        coinPool.PrintStackCount();
        treeSpawnPos = new Vector3(0, 0, 0);
        // Spawn the trees on start. They'll use their
        // own scripts to move themselves later on.
        SpawnTrees();
    }


    // //////////////////////////////////////
    // ////////////// METHODS ///////////////
    // //////////////////////////////////////

    // This method is to instantiate a map.
    public void SpawnMap(){
        spawnedMapNumber++;
        mapSpawnPos = 
                new Vector3(0, 
                            0, 
                            (spawnedMapNumber) * 182);
        Instantiate(map, mapSpawnPos, Quaternion.identity);
    }

    // This method is to spawn the trees on the start of 
    // the game.
    void SpawnTrees(){
        Instantiate(trees, treeSpawnPos, Quaternion.identity);
    }

    // This method is to pop the coins from the pool and to show them.
    public void PopCoinFromThePool(){
        for(int i = 0; i < 6; i++){
            // Get two random values.
            int j1 = Random.Range(0,4); 
            int j2 = Random.Range(0,4);

            // If the values are the same, change one of them.
            while(j1 == j2){
                j2 = Random.Range(0,4);
            }

            // Pop the first coin pack.
            offset = Random.Range(10f,20f);
            for(int j = 0; j < 4; j++){
                coinSpawnPos = new Vector3(j1*1.8f-0.3f, 
                                0.5f, 
                                spawnedMapNumber*182 + j*2.5f + i*30 + offset - 200);
                coin = coinPool.GetFromThePool();
                coin.transform.position = coinSpawnPos;
                coin.transform.rotation = Quaternion.identity;
            }

            // Pop the second coin pack.
            offset = Random.Range(10f,20f);
            for(int j = 0; j < 4; j++){
                coinSpawnPos = new Vector3(j2*1.8f-0.3f, 
                                0.5f, 
                                spawnedMapNumber*182 + j*2.5f + i*30 + offset - 200);
                coin = coinPool.GetFromThePool();
                coin.transform.position = coinSpawnPos;
                coin.transform.rotation = Quaternion.identity;
            }
        }
    }

    // This method is to add a coin pack object to the pool.
    public void AddCoinToThePool(GameObject coinObject){
        coinPool.AddToThePool(coinObject);
    }
}
