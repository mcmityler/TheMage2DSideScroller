using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour
{
    private bool _collected = false;
    public int manapickupAmount = 1;
    [SerializeField] AudioClip manaRefilSound;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_collected && col.CompareTag("Player"))
        {
            _collected = true;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManaScript>().ManaPickup(manapickupAmount);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(manaRefilSound);
            Destroy(this.gameObject);

        }
    }
}
