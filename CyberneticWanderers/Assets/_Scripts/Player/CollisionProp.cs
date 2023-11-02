using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProp : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        // Получение компонента Rigidbody2D персонажа
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

}
