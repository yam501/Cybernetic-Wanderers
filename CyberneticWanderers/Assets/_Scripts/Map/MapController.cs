using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> groundChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noGroundPositions;
    public LayerMask groundMask;
    public GameObject currentChunck;
    private PlayerMovement pm;
    [Header("Optimization")] public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist;
    float opDist; 
    float opCooldown;
    public float optCoolddownDur;
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
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
        
        if (pm.moveDir.x > 0 && pm.moveDir.y == 0) //right
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Right").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Right").position;
                SpawnChunck();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0) //left
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Left").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Left").position;
                SpawnChunck();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y > 0) //up
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Up").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Up").position;
                SpawnChunck();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y < 0) //down
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Down").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Down").position;
                SpawnChunck();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0) //right up
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Right Up").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Right Up").position;
                SpawnChunck();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0) //right down
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Right Down").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Right Down").position;
                SpawnChunck();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0) //left up
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Left Up").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Left Up").position;
                SpawnChunck();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0) //left down
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Left Down").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Left Down").position;
                SpawnChunck();
            }
        }
    }

    void SpawnChunck()
    {
        int rand = Random.Range(0, groundChunks.Count);
        latestChunk =  Instantiate(groundChunks[rand], noGroundPositions, Quaternion.identity);
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
}
