using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 100;
    public void Hit()
    {
        Destroy(gameObject);
    }
    public float getDamage()
    {
        return damage;
    }

}

