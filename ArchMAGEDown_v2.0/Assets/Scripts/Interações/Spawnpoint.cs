using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime;
    public float spawnRate;
    public Transform spawnPoints;


    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnRate);
    }


    void Spawn()
    {
        Instantiate(enemy, spawnPoints.position, spawnPoints.rotation);
    }

}
