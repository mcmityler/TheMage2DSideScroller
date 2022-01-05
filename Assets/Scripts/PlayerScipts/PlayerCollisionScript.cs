using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Script to handle killing player
 * 
 */
public class PlayerCollisionScript : MonoBehaviour
{

    //Reference to game manager
    GameObject _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager");//get reference
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) //Check if it has enemy tag
        {
            _gameManager.GetComponent<HealthScript>().TakeDamage(1);//Kill player

        }
        if (other.gameObject.CompareTag("EnemyBase")) //Check if it has enemy tag
        {
            _gameManager.GetComponent<HealthScript>().TakeDamage(1);//Kill player

        }
        if (other.gameObject.CompareTag("Death")) //Check if it has death tag
        {
            _gameManager.GetComponent<HealthScript>().TakeDamage(100);//Kill player
        }
        if (other.gameObject.CompareTag("Spike")) //Check if it has death tag
        {
            _gameManager.GetComponent<HealthScript>().TakeDamage(2);//Kill player
        }
    }

    public void pCollisionOutside(int dmg)
    {
        _gameManager.GetComponent<HealthScript>().TakeDamage(dmg);

    }

}
