using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {
    private GameObject[] spawners;
    public GameObject zombiePrefab;
    private float spawnTime;
	// Use this for initialization
	void Awake () {
        spawners = GameObject.FindGameObjectsWithTag("Spawners");
        spawnTime = Random.Range(0f, 3.2f) + Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        checkSpawn();
	}
    
    void checkSpawn ()
    {
        if (spawnTime < Time.time)
        {
            spawnZombie();
        }
    }

    void spawnZombie ()
    {
        Instantiate(zombiePrefab, spawners[Random.Range(0, 4)].transform.position, Quaternion.identity);
        spawnTime = Random.Range(0f, 4.8f) + Time.time;
    }
}

