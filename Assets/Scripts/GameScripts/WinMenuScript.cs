using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Controls and Such for win Screen
 * 
 */
public class WinMenuScript : MonoBehaviour
{
    //Have you won the game
    public bool gameWon = false;
    //Win arrow showing?
    private bool _warrowShowing = false;
    //win arrow image
    public Image warrowImg;
    //win arrow poins
    public GameObject[] warrowPoints;
    //what arrow are you currently on
    private int _currentArrow = 0;
    //Win panel
    public GameObject winPanel;
    //Score text
    public Text scoreTxt;
    //final score 
    private int _finalScore = 0;
    private int _counter = 0;

    public Text coinAmountText;
    public Text coinScoreText;
    public Text timeAmountText;
    public Text timeScoreText;
    public Text enemiesAmountText;
    public Text enemiesScoreText;
    public Text heartAmountText;
    public Text heartScoreText;


    private float _timer = 1.5f;
    private float _timePerScore = 2;
    private int _currentScore = 0; // 0 = nothing, 1 = coins, 2 = time, 3 = enemies
    void Awake()
    {
        coinAmountText.transform.parent.gameObject.SetActive(false);
        timeAmountText.transform.parent.gameObject.SetActive(false);
        enemiesAmountText.transform.parent.gameObject.SetActive(false);
        heartAmountText.transform.parent.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //Have you won game
        if (gameWon)
        {


            //Update score text box
            scoreTxt.text = _counter.ToString();
            //Win arrow 
            WinArrow();
            if (_counter == _finalScore)
            {
                if (_currentScore == 0)
                {
                    _currentScore++;
                    timeAmountText.transform.parent.gameObject.SetActive(true);
                    //set final score
                    _finalScore = (int)gameObject.GetComponent<Score>().timeLeft * 10;


                }
                else if (_currentScore == 1)
                {
                    _currentScore++;
                    coinAmountText.transform.parent.gameObject.SetActive(true);
                    //set final score
                    _finalScore += gameObject.GetComponent<PlayerInfoScript>().coins * 100;
                }
                else if (_currentScore == 2)
                {
                    _currentScore++;
                    enemiesAmountText.transform.parent.gameObject.SetActive(true);
                    _finalScore += gameObject.GetComponent<Score>().enemyCount * 250;
                }
                else if (_currentScore == 3)
                {
                    _currentScore++;
                    heartAmountText.transform.parent.gameObject.SetActive(true);
                    _finalScore = gameObject.GetComponent<Score>().playerScore;
                }
            }

            //Increase counter like in mario
            if (_counter < _finalScore)
            {
                _counter += 5;
            }


        }
    }
    //Win game function
    public void WinGame()
    {
        //Pause game time
        Time.timeScale = 0f;

        //set panel active
        winPanel.SetActive(true);
        //win game
        gameWon = true;

        coinAmountText.text = gameObject.GetComponent<PlayerInfoScript>().coins.ToString();
        int _coinScoreTemp = gameObject.GetComponent<PlayerInfoScript>().coins * 100;
        coinScoreText.text = _coinScoreTemp.ToString();

        int _timeTemp = (int)gameObject.GetComponent<Score>().timeLeft;
        timeAmountText.text = _timeTemp.ToString();

        int _timeScoreTemp = _timeTemp * 10;
        timeScoreText.text = _timeScoreTemp.ToString();

        enemiesAmountText.text = gameObject.GetComponent<Score>().enemyCount.ToString();
        int _enemyScoreTemp = gameObject.GetComponent<Score>().enemyCount * 250;
        enemiesScoreText.text = _enemyScoreTemp.ToString();

        heartAmountText.text = gameObject.GetComponent<HealthScript>().GetPlayerHealth().ToString();
        int _heartScoreTemp = gameObject.GetComponent<HealthScript>().GetPlayerHealth() * 50;
        heartScoreText.text = _heartScoreTemp.ToString();

    }
    //Function for if you quickly want score to display on win screen
    void QuickScore()
    {
        coinAmountText.transform.parent.gameObject.SetActive(true);
        timeAmountText.transform.parent.gameObject.SetActive(true);
        enemiesAmountText.transform.parent.gameObject.SetActive(true);
        heartAmountText.transform.parent.gameObject.SetActive(true);
        _finalScore = gameObject.GetComponent<Score>().playerScore;
        _counter = _finalScore;
        Time.timeScale = 0;
    }
    //Controls and such for Win arrow
    void WinArrow()
    {
        if (!_warrowShowing)
        {
            warrowImg.enabled = false;
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0))
            {
                _warrowShowing = true;
                //If you press key auto sets final score
                QuickScore();

            }

        }
        else if (_warrowShowing)
        {
            warrowImg.enabled = true;
            if (Input.GetButtonDown("Up"))
            {
                if (_currentArrow > 0)
                {
                    _currentArrow--;
                }
            }
            else if (Input.GetButtonDown("Down"))
            {
                if (_currentArrow < warrowPoints.Length - 1)
                {
                    _currentArrow++;
                }
            }
            //update position of arrow
            warrowImg.transform.position = warrowPoints[_currentArrow].transform.position;
            //Select button
            if (Input.GetButtonDown("Select"))
            {
                switch (_currentArrow)
                {
                    case 0:
                        //level select
                        Time.timeScale = 1f;

                        gameObject.GetComponent<Manager>().GotoLevelSelect();
                        break;
                    case 1: //main menu
                        Time.timeScale = 1f;
                        gameObject.GetComponent<Manager>().GotoMainMenu();
                        break;

                }
            }
        }
    }
}
