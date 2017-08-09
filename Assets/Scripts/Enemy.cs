using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    public event UnityAction<Collision> TakeDamage;
    public int hitpoints = 2;

    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage(collision);
    }
}
