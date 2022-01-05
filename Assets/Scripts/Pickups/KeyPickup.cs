using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private bool _collected = false;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_collected && col.CompareTag("Player"))
        {
            _collected = true;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Manager>().KeyCollected();
            Destroy(this.gameObject);
        }
    }
}
