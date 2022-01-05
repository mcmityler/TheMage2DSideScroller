using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Bounce the enemy back.
 * 
 */
public class EnemyBounceBack : MonoBehaviour
{
    //Enemy gameobject
    public GameObject _enemy;
    [SerializeField] private LayerMask _bounceback;
    // Start is called before the first frame update
    void Start()
    {
        //Reference to enemy game object (self gameobject)
        _enemy = this.transform.parent.gameObject;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            _enemy.GetComponent<BaseEnemy>().FlipEnemy();//Flip enemy if triggers hit
        }
        if (other.CompareTag("NoheadCol"))
        {
            _enemy.GetComponent<BaseEnemy>().FlipEnemy();//Flip enemy if triggers hit
        }
        if (other.CompareTag("EnemyBase"))
        {
            _enemy.GetComponent<BaseEnemy>().FlipEnemy();//Flip enemy if triggers hit
        }
        if (other.CompareTag("Enemy"))
        {
            _enemy.GetComponent<BaseEnemy>().FlipEnemy();//Flip enemy if triggers hit
        }

    }
}
