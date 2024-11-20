using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Patient : MonoBehaviour
{
    public float health = 80f; // Default health value
    public float distance;

    // Method to decrease health
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Method to handle the patient's death
    void Die()
    {
        //Debug.Log($"{gameObject.name} has died.");
        UnityEngine.Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }
}
