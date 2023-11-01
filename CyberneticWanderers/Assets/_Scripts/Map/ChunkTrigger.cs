using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapController mc;
    public GameObject targetMap;


    void Start()
    {
        mc = FindObjectOfType<MapController>();
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("OnTriggerStay!");
        if (col.CompareTag("Player"))
        {
            mc.currentChunck = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("OnTriggerStay!");

        if (col.CompareTag("Player"))
        {
            if (mc.currentChunck == targetMap)
            {
                mc.currentChunck = null;
            }
        }
    }
}
