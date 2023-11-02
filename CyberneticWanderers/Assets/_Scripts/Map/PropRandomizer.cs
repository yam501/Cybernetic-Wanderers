using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefab;

    void Start()
    {
        SpawnProps();
    }


    void Update()
    {
    }

    void SpawnProps()
    {
        try
        {
            foreach (GameObject sp in propSpawnPoints)
            {
                int rand = Random.Range(0, propPrefab.Count);
                GameObject prop = Instantiate(propPrefab[rand], sp.transform.position, Quaternion.identity);
                prop.transform.parent = sp.transform;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}