using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Fireball Spell of player
 * 
 */
public class FBScript : MonoBehaviour
{
    //Force in the x and y direction respec. the fireball will shoot.
    private Transform tempTrans;
    [SerializeField] private float bulletSpeed = 2;
    // Start is called before the first frame update
    [SerializeField] private AudioClip fireballSound;
    void Start()
    {

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(fireballSound);

    }
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * bulletSpeed;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerCollisionScript>().pCollisionOutside(1);
            Destroy(gameObject);
        }
        else if (!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

        }

    }


}
