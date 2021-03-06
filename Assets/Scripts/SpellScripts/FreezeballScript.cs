using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Fireball Spell of player
 * 
 */
public class FreezeballScript : MonoBehaviour
{
    //Which direction is the player facing (updates in the player controller, when fireball is created)
    public bool shootRight = false;
    //Force in the x and y direction respec. the fireball will shoot.
    public int xFireForce = 0;
    public int yFireForce = 0;
    // Start is called before the first frame update
    [SerializeField] private AudioClip fireballSound;
    void Start()
    {

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(fireballSound);
        if (shootRight)
        {

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xFireForce, yFireForce));
        }
        if (!shootRight)
        {

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-xFireForce, yFireForce));
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBase"))
        {
            other.gameObject.GetComponent<BaseEnemy>().Freeze();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<PathEnemyScript>() != null)
            {
                other.gameObject.GetComponent<PathEnemyScript>().Freeze();
            }
            if (other.gameObject.GetComponent<MultiDirectionalShooter>() != null)
            {
                other.gameObject.GetComponent<MultiDirectionalShooter>().Freeze();
            }
            if (other.gameObject.GetComponent<SingleDirectionalEnemy>() != null)
            {
                other.gameObject.GetComponent<SingleDirectionalEnemy>().Freeze();
            }
            Destroy(gameObject);
        }
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }


}
