using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public List<GameObject> piecePrefabs;

    void Awake() {
        instance = this;   
    }

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Instantiate(piecePrefabs[(int)Random.Range(0, piecePrefabs.Count-1)], transform.position, transform.rotation);
    }
}
