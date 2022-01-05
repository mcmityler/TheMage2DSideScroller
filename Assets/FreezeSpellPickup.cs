using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSpellPickup : MonoBehaviour
{
    private bool _collected = false;
    [SerializeField] private AudioClip ScrollSound;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_collected && col.CompareTag("Player"))
        {
            _collected = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>().freezeScrollAquired = true;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(ScrollSound);

            Destroy(this.gameObject);
        }
    }
}
