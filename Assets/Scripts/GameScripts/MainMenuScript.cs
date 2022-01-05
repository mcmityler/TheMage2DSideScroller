using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
* Written by: Tyler McMillan
* Purpose of script:
* Main Menu Script
* Scripts for buttons on main menu and anything needed on main menu
*/

public class MainMenuScript : MonoBehaviour
{

    private bool _arrowShowing = false; //is the arrow showing on the menu screen
    public Image arrowImg; //arrow image
    public GameObject[] arrowPoints;
    private int _currentArrow = 0;

    //Resets save info and starts game
    public void StartGame()
    {
        gameObject.GetComponent<PlayerInfoScript>().resetSaveInfo();
        SceneManager.LoadScene("LevelSelect");
    }
    //Load game from save
    public void LoadGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    //Load control/Help screen
    public void Controls()
    {

    }
    //Quit application
    public void QuitGame()
    {
        Application.Quit();
    }


    void Update()
    {
        //Update mainmenu arrow
        if (!gameObject.GetComponent<QuitMenuScript>().quitGame)
        {
            MenuArrow();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameObject.GetComponent<QuitMenuScript>().quitGame)
        {
            gameObject.GetComponent<QuitMenuScript>().QuitGameMenu();
        }
    }

    void MenuArrow()
    {
        if (!_arrowShowing)
        {
            arrowImg.enabled = false;
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Escape))
            {
                _arrowShowing = true;
            }
        }
        else if (_arrowShowing)
        {
            arrowImg.enabled = true;
            if (Input.GetButtonDown("Up"))
            {
                if (_currentArrow > 0)
                {
                    _currentArrow--;
                }
            }
            else if (Input.GetButtonDown("Down"))
            {
                if (_currentArrow < arrowPoints.Length - 1)
                {
                    _currentArrow++;
                }
            }
            arrowImg.transform.position = arrowPoints[_currentArrow].transform.position;
            if (Input.GetButtonDown("Select"))
            {
                switch (_currentArrow)
                {
                    case 0:
                        StartGame();
                        break;
                    case 1:
                        LoadGame();
                        break;
                    case 2:
                        Controls();
                        break;
                    case 3:
                        gameObject.GetComponent<QuitMenuScript>().QuitGameMenu();
                        break;

                }
            }
        }
    }
}