using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Coin object script
 * 
 */
public class CoinScript : MonoBehaviour
{
    public Sprite[] coinAnim;
    public float cTimeToAnim = 0;
    public float animationTime = 10;
    private int _currentSprite = 0;
    public float timeperSprite = 0.1f;
    private bool _animating = false;
    [SerializeField] private AudioClip coinSound;
    //Reference to gamemanager for score
    private GameObject _gameManager;
    //have you collected the coin already (use this because of problem of player colliding with same coin twice(two colliders))
    private bool _collected = false;
    void Start()
    {
        //Get reference
        _gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
    void Update()
    {
        cTimeToAnim += Time.deltaTime;
        if (cTimeToAnim <= animationTime && !_animating)
        {
            _currentSprite = 0;
        }
        if (cTimeToAnim >= animationTime && !_animating)
        {
            cTimeToAnim = 0;
            _animating = true;
        }
        else if (cTimeToAnim > timeperSprite && _animating)
        {
            if (_currentSprite < coinAnim.Length - 1)
            {
                _currentSprite++;
            }
            else
            {
                _animating = false;
            }
            cTimeToAnim = 0;

        }
        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = coinAnim[_currentSprite];
    }

    //has the player entered the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check tag
        if (other.CompareTag("Player"))
        {
            //Have you already got?
            if (!_collected)
            {
                _gameManager.GetComponent<AudioSource>().PlayOneShot(coinSound);
                //got it
                _collected = true;
                //delete coin
                Destroy(gameObject);
                //Increase score and coin count
                _gameManager.GetComponent<Score>().CoinCollected();
            }
        }
    }
}
