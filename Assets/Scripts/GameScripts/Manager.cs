using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
* Written by: Tyler McMillan
* Purpose of script:
* Game Manager, Manages game essentials that have to be there all the time and cant be stored on something that needs to be destroyed.
* 
*/
public class Manager : MonoBehaviour
{
    //Is pause Arrow Showing
    private bool _parrowShowing = false;
    //Reference to pause arrow image
    public Image parrowImg;
    //Current arrow position
    private int _currentArrow = 0;
    //Arrow positions
    public GameObject[] parrowPoints;

    public GameObject keylockImg;
    public Sprite keyCollectSprite;
    public bool keyPickedUp = false;


    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Key"))
        {
            keylockImg.GetComponent<Image>().enabled = true;
        }
        else
        {
            keylockImg.GetComponent<Image>().enabled = false;
        }
        gameObject.GetComponent<PlayerInfoScript>().piLoadInfo();
    }
    public void KeyCollected()
    {
        keylockImg.GetComponent<Image>().sprite = keyCollectSprite;
        keyPickedUp = true;
    }
    // Update is called once per frame
    void Update()
    {
        //Check if game is paused
        if (gameObject.GetComponent<PauseScript>().gamePaused == true)
        {
            //Run pause arrow function
            PauseArrow();
        }
        else
        {
            //Reset values if not paused
            _parrowShowing = false;
            _currentArrow = 0;
        }
        //Unpause if unpause buttons clicked && makes sure you cant pause when dead or when game is won
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !gameObject.GetComponent<DeathMenuScript>().playerDead && !gameObject.GetComponent<WinMenuScript>().gameWon)
        {
            gameObject.GetComponent<PauseScript>().Paused();
        }
    }
    //Go to main menu 
    public void GotoMainMenu()
    {
        //Check if pause/if paused unpause
        if (gameObject.GetComponent<PauseScript>().gamePaused)
        {
            gameObject.GetComponent<PauseScript>().Paused();
        }
        //Resume timescale just to be safe
        Time.timeScale = 1f;
        //Load main menu
        SceneManager.LoadScene("MainMenu");
    }
    //Go to level select
    public void GotoLevelSelect()
    {
        //Check if pause/if paused unpause
        if (gameObject.GetComponent<PauseScript>().gamePaused)
        {
            gameObject.GetComponent<PauseScript>().Paused();
        }
        //Resume time scale to make sure
        Time.timeScale = 1f;
        //Load level select screen
        SceneManager.LoadScene("LevelSelect");
    }

    //Pause Arrow Script
    void PauseArrow()
    {
        //Is pause arrow showing
        if (!_parrowShowing)
        {
            //Make sure image disabled
            parrowImg.enabled = false;
            //Check if any key pressed but left click
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Show arrow
                _parrowShowing = true;
            }

        }
        //Is Pause arrow showing
        else if (_parrowShowing)
        {
            //Enable image 
            parrowImg.enabled = true;
            //Controls to browse menu
            if (Input.GetButtonDown("Up"))
            {
                if (_currentArrow > 0)
                {
                    _currentArrow--;
                }
            }
            else if (Input.GetButtonDown("Down"))
            {
                if (_currentArrow < parrowPoints.Length - 1)
                {
                    _currentArrow++;
                }
            }
            //Update arrow location
            parrowImg.transform.position = parrowPoints[_currentArrow].transform.position;
            //If you select a button with keys
            if (Input.GetButtonDown("Select"))
            {
                switch (_currentArrow)
                {
                    case 0:
                        //Resume Game
                        gameObject.GetComponent<PauseScript>().Paused();
                        break;
                    case 1:
                        //Goto level select
                        GotoLevelSelect();
                        break;
                    case 2:
                        //Goto main menu
                        GotoMainMenu();
                        break;

                }
            }
        }
    }
}
