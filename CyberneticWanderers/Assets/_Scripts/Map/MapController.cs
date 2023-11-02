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
    private PlayerMovement pc;
    
    void Start()
    {
        pc = FindObjectOfType<PlayerMovement>();
    }

   
    void Update()
    {
        ChunkChecker();
    }

    void ChunkChecker()
    {
        Debug.Log(currentChunck);

        if (!currentChunck)
        {
            return;
        }
        
        if (pc.moveDir.x > 0 && pc.moveDir.y == 0) //right
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Right").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Right").position;
                SpawnChunck();
            }
        }
        else if (pc.moveDir.x < 0 && pc.moveDir.y == 0) //left
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Left").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Left").position;
                SpawnChunck();
            }
        }
        else if (pc.moveDir.x == 0 && pc.moveDir.y > 0) //up
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Up").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Up").position;
                SpawnChunck();
            }
        }
        else if (pc.moveDir.x == 0 && pc.moveDir.y < 0) //down
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Down").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Down").position;
                SpawnChunck();
            }
        }
        else if (pc.moveDir.x > 0 && pc.moveDir.y > 0) //right up
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Right Up").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Right Up").position;
                SpawnChunck();
            }
        }
        else if (pc.moveDir.x > 0 && pc.moveDir.y < 0) //right down
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Right Down").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Right Down").position;
                SpawnChunck();
            }
        }
        else if (pc.moveDir.x < 0 && pc.moveDir.y > 0) //left up
        {
            if (!Physics2D.OverlapCircle(currentChunck.transform.Find("Left Up").position, checkerRadius, groundMask))
            {
                noGroundPositions = currentChunck.transform.Find("Left Up").position;
                SpawnChunck();
            }
        }
        else if (pc.moveDir.x < 0 && pc.moveDir.y < 0) //left down
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
        Instantiate(groundChunks[rand], noGroundPositions, Quaternion.identity);
    }
}
