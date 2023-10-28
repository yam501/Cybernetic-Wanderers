using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [Header("Weapon Stats")]
    public GameObject prefab;
    public float damage, speed, cooldownDuration;
    public int pierce;
    float currentCooldown;
    

    void Start()
    {
        currentCooldown = cooldownDuration;
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    void Attack()
    {
        currentCooldown = cooldownDuration;
    }
}
