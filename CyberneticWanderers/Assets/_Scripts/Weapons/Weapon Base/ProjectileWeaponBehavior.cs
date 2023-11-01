using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehavior : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;
    public float spriteAngleOffset;


    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        //Нахождение угла полёта снаряда, для каждого префаба нужно найти spriteAngleOffset - угол отклонения
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += spriteAngleOffset;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
