using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Health pickup script
 * 
 */
public class HealthPickup : MonoBehaviour
{

    public int healPickupAmount = 1;
    private bool _collected = false;
    [SerializeField] AudioClip healthRefilSound;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_collected && col.CompareTag("Player"))
        {
            _collected = true;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<HealthScript>().HealHealth(healPickupAmount);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(healthRefilSound);
            Destroy(this.gameObject);
        }
    }
}
