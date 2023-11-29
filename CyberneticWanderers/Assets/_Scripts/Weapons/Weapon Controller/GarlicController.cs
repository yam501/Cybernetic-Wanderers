using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGralic = Instantiate(weaponData.prefab);
        spawnedGralic.transform.position = transform.position;
        spawnedGralic.transform.parent = transform;
    }
}
