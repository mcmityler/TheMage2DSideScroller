using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
* Written by: Tyler McMillan
* Purpose of script:
* Death Screen controls, and death scripts.
* 
*/

public class DeathMenuScript : MonoBehaviour
{
    //is the player dead;
    public bool playerDead = false;
    //is the death arrow showing
    private bool _darrowShowing = false;
    //Image for death arrow
    [SerializeField] private Image _darrowImg;
    //What arrow is it currently on
    private int _currentArrow = 0;
    //Locations of arrow
    [SerializeField] private GameObject[] _darrowPoints;
    //Death Menu Panel reference
    [SerializeField] private GameObject _deathPanel;


    //Make sure variables are right when script is booted up
    void Awake()
    {
        //Make sure player is alive
        playerDead = false;
    }

    void Update()
    {
        //Is the player dead
        if (playerDead)
        {
            //Update death arrow (position + controls)
            DeathArrow();
        }
    }
    //Script to launch player Death screen
    public void DeathScreen()
    {
        playerDead = true;
        Time.timeScale = 0f;
        _deathPanel.SetActive(true);
    }
    //Death arrow Script
    void DeathArrow()
    {
        //Is the arrow already showing?
        if (!_darrowShowing)
        {
            //make sure object isnt enabled
            _darrowImg.enabled = false;
            //If any key pressed show arrow
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0))
            {
                _darrowShowing = true;
            }
        }
        //Is arrow Showing?
        else if (_darrowShowing)
        {
            //Enable game object
            _darrowImg.enabled = true;
            //Controls to go through screen
            if (Input.GetButtonDown("Up"))
            {
                if (_currentArrow > 0)
                {
                    _currentArrow--;
                }
            }
            else if (Input.GetButtonDown("Down"))
            {
                if (_currentArrow < _darrowPoints.Length - 1)
                {
                    _currentArrow++;
                }
            }
            //Update position of death arrow
            _darrowImg.transform.position = _darrowPoints[_currentArrow].transform.position;
            //Check if you selected a button
            if (Input.GetButtonDown("Select"))
            {
                //Set player alive
                playerDead = false;
                //Check which arrow its on
                switch (_currentArrow)
                {
                    case 0:
                        //reset scene
                        RestartLevel();
                        break;
                    case 1:
                        //go to lvl select
                        gameObject.GetComponent<Manager>().GotoLevelSelect();
                        break;
                    case 2:
                        //go to main menu
                        gameObject.GetComponent<Manager>().GotoMainMenu();
                        break;

                }
            }
        }
    }
    //Function to restart level from button press.
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
