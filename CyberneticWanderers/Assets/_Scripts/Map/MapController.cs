using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> groundChunks;
    public GameObject player;
    public float checkerRadius;
    
    public LayerMask groundMask;
    public GameObject currentChunck;

    Vector3 playerLastPosition;



    [Header("Optimization")] public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist;
    float opDist; 
    float opCooldown;
    public float optCoolddownDur;
    void Start()
    {
        playerLastPosition = player.transform.position;
    }

   
    void Update()
    {
        ChunkChecker();
        ChunkOptimization();
    }

    void ChunkChecker()
    {
        
        if (!currentChunck)
        {
            return;
        }
        
        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDir);



        CheackAndSpawnChunk(directionName);

        if (directionName.Contains("Up"))
        {
            CheackAndSpawnChunk("Up");
        }
        if (directionName.Contains("Down"))
        {
            CheackAndSpawnChunk("Down");
        }
        if (directionName.Contains("Right"))
        {
            CheackAndSpawnChunk("Right");
        }
        if (directionName.Contains("Left"))
        {
            CheackAndSpawnChunk("Left");
        }



    }


    void CheackAndSpawnChunk(string direction)
    {
        if (!Physics2D.OverlapCircle(currentChunck.transform.Find(direction).position, checkerRadius, groundMask))
        {
            SpawnChunck(currentChunck.transform.Find(direction).position);
        }
    }

    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.y > 0.5f)
            {
                return direction.x > 0 ? "Right Up" : "Left Up";
            }
            else if(direction.y < -0.5f)
            {
                return direction.x > 0 ? "Right Down" : "Left Down";
            }
            else
            {
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            if (direction.x > 0.5f)
            {
                return direction.y > 0 ? "Right Up" : "Right Down";
            }
            else if (direction.x < -0.5f)
            {
                return direction.y > 0 ? "Left Up" : "Left Down";
            }
            else
            {
                return direction.y > 0 ? "Up" : "Down";
            }
        }


    }




    void SpawnChunck(Vector3 spawnPosition)
    {
        int rand = UnityEngine.Random.Range(0, groundChunks.Count);
        latestChunk =  Instantiate(groundChunks[rand], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimization()
    {
        optCoolddownDur -= Time.deltaTime;

        if (opCooldown <= 0f)
        {
            opCooldown = optCoolddownDur;
        }
        else
        {
            return;
        }

        try
        {
            foreach (GameObject chunk in spawnedChunks)
            {
                opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
                if (opDist > maxOpDist)
                {
                    chunk.SetActive(false);
                }
                else
                {
                    chunk.SetActive(true);
                }
            }
        }
        catch (Exception e) 
        {

        }
        
    }
}
