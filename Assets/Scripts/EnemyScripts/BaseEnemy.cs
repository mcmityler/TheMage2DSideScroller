using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Base enemy that will just move in the direction you want until it cant go that way any more.
 * 
 */
public class BaseEnemy : MonoBehaviour
{
    public int enemySpeed; //Speed of enemy
    public int xMoveDirection; //Which way is it moving
    [SerializeField] private Collider2D _RightWallCollider;//Reference to rightwallCollider for bounce back
    [SerializeField] private Collider2D _LeftWallCollider;//Reference to leftwallCollider for bounce back
    //Get current velocity for y 
    private Vector2 _temp;
    private Vector2 _moving;
    //refectence to game manager
    private GameObject manager;


    [SerializeField] private GameObject frozenBlock;
    [SerializeField] private Sprite[] fBlockAnim;
    private int fAnimCount = 0;
    private bool frozen = false;
    private float freezeCounter = 0;
    [SerializeField] private float freezeTime = 2;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager");
        frozenBlock.SetActive(false);
        fAnimCount = 0;
        freezeCounter = 0;
        frozen = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (manager.GetComponent<PauseScript>().gamePaused == false && manager.GetComponent<WinMenuScript>().gameWon == false && !frozen)
        {
            _temp = this.gameObject.GetComponent<Rigidbody2D>().velocity;
            //Get the velocity the enemy will be moving in the x direction
            _moving = new Vector2(xMoveDirection, 0) * enemySpeed;
            //Update the velocity of the enemy
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(_moving.x, _temp.y);

        }
        if (frozen)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            freezeCounter += Time.deltaTime;
            if (freezeCounter >= freezeTime)
            {
                freezeCounter = 0;
                fAnimCount++;
                if (fAnimCount >= fBlockAnim.Length)
                {
                    frozenBlock.SetActive(false);
                    frozen = false;
                    fAnimCount = 0;
                    gameObject.tag = "EnemyBase";
                }
                frozenBlock.GetComponent<SpriteRenderer>().sprite = fBlockAnim[fAnimCount];
            }
        }

    }
    public void FlipEnemy()
    {
        xMoveDirection = -xMoveDirection; //Change direction of enemy
    }
    public void killEnemy()
    {
        manager.GetComponent<Score>().enemyCount++; //increase players enemy kill count
        Destroy(gameObject); //Kill enemy
    }
    public void Freeze()
    {
        frozen = true;
        gameObject.tag = "Ground";
        frozenBlock.SetActive(true);
        frozenBlock.transform.position = gameObject.transform.position;
        fAnimCount = 0;
        freezeCounter = 0;
    }
}
