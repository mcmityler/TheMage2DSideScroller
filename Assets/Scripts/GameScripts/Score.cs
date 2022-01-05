using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Updates score of game text and timer text
 * 
 */
public class Score : MonoBehaviour
{
    //Time left in game
    public float timeLeft = 40;
    //Current score of player
    public int playerScore = 0;
    //Has the player finished the level
    public bool finishedLvl = false;
    //Time text
    public GameObject timeLeftUI;
    //Score text
    public GameObject scoreUI;
    //Coin Text
    public GameObject coinUI;
    //makes sure gameover panel doesnt run multiple times
    bool _gameover = false;

    public int enemyCount = 0;


    void Awake()
    {
        _gameover = false;
    }
    void Update()
    {
        if (gameObject.GetComponent<PauseScript>().gamePaused == false)
        {
            //Update text boxes
            timeLeftUI.gameObject.GetComponent<Text>().text = ("Time left: " + (int)timeLeft);
            scoreUI.gameObject.GetComponent<Text>().text = ("Score: " + playerScore);
            coinUI.gameObject.GetComponent<Text>().text = ("Coins: " + gameObject.GetComponent<PlayerInfoScript>().coins);
            //Demish time if level isnt finished
            if (!finishedLvl)
            {
                timeLeft -= Time.deltaTime;
            }
            //Kill player if score reaches 0
            if (timeLeft <= 0 && _gameover == false)
            {
                //makes sure doesnt run multiple times
                _gameover = true;
                gameObject.GetComponent<DeathMenuScript>().DeathScreen();
            }
        }
    }
    public void scoreKillEnemy()
    {
        playerScore = playerScore + (enemyCount * 250);
    }
    public void scorePlayerHealth()
    {
        playerScore = playerScore + (gameObject.GetComponent<HealthScript>().GetPlayerHealth() * 50);

    }
    //Update score if beat game before time runs out
    public void TimeScore()
    {
        playerScore = playerScore + ((int)timeLeft * 10);
    }
    //Coin Collection function, adds to score and values of coins.
    public void CoinCollected()
    {
        playerScore = playerScore + 100;
        gameObject.GetComponent<PlayerInfoScript>().coins++;
    }
}
