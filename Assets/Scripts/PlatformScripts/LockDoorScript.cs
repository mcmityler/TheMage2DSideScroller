using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorScript : MonoBehaviour
{

    private bool _collected = false;
    [SerializeField] AudioClip unlockSound;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_collected && col.CompareTag("Player") && GameObject.FindGameObjectWithTag("GameManager").GetComponent<Manager>().keyPickedUp)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(unlockSound);
            _collected = true;
            Destroy(this.gameObject);
        }
    }
}
