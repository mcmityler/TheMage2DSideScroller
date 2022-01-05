using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUpgrade : MonoBehaviour
{
    public int manapickupNumber = 0;
    private bool _collected = false;
    [SerializeField] private AudioClip manaUpSound;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_collected && col.CompareTag("Player"))
        {
            if (manapickupNumber == 1 && !GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInfoScript>().ManaContainer1)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInfoScript>().ManaContainer1 = true;
                collect();
            }
            if (manapickupNumber == 2 && !GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInfoScript>().ManaContainer2)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInfoScript>().ManaContainer2 = true;
                collect();
            }
            if (manapickupNumber == 3 && !GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInfoScript>().ManaContainer3)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerInfoScript>().ManaContainer3 = true;
                collect();
            }

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(manaUpSound);
            _collected = true;
            Destroy(this.gameObject);
        }
    }
    void collect()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManaScript>().ManaIncrease();
    }

}
