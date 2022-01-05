using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Heart Container Pickup
 * 
 */
public class HeartUpgrade : MonoBehaviour
{
    public int heartpickupNumber = 0;
    private bool _collected = false;
    [SerializeField] AudioClip healthUpSound;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_collected && col.CompareTag("Player"))
        {
            if (heartpickupNumber == 1 && !GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInfoScript>().HeartContainer1)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInfoScript>().HeartContainer1 = true;
                collect();
            }
            if (heartpickupNumber == 2 && !GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInfoScript>().HeartContainer2)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInfoScript>().HeartContainer2 = true;
                collect();
            }

            Destroy(this.gameObject);
            _collected = true;

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(healthUpSound);
        }
    }
    void collect()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<HealthScript>().AddHeartContainer();
    }
}
