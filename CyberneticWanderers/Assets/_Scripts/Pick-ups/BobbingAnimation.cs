using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnimation : MonoBehaviour
{
    public float frequency;
    public float magnitude;
    public Vector3 direction;
    Vector3 initalPosition;
    Pickup pickup;


    // Start is called before the first frame update
    void Start()
    {
        pickup = GetComponent<Pickup>();
        initalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickup && !pickup.hasBeenCollected) {
        
            transform.position = initalPosition + direction * Mathf.Sin(Time.time*frequency) * magnitude;
        
        }
    }
}
