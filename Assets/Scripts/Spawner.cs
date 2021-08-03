using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public List<GameObject> piecePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void Spawn()
    {
        Instantiate(piecePrefabs[(int)Random.Range(0, piecePrefabs.Count-1)], transform.position, transform.rotation);
    }
}
