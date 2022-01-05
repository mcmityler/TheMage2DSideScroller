using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitMenuScript : MonoBehaviour
{
    //Have you won the game
    public bool quitGame = false;
    //Win arrow showing?
    private bool _qarrowShowing = false;
    //win arrow image
    public Image qarrowImg;
    //win arrow poins
    public GameObject[] qarrowPoints;
    //what arrow are you currently on
    private int _currentArrow = 0;
    //Win panel
    public GameObject quitPanel;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (quitGame)
        {
            QuitArrow();
        }

    }
    public void QuitGameMenu()
    {
        //set panel active
        quitPanel.SetActive(true);
        //win game
        quitGame = true;
    }
    public void NoButton()
    {
        quitPanel.SetActive(false);
        quitGame = false;
        _qarrowShowing = false;
        _currentArrow = 0;
    }

    void QuitArrow()
    {
        if (!_qarrowShowing)
        {
            qarrowImg.enabled = false;
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Escape))
            {
                _qarrowShowing = true;

            }

        }
        else if (_qarrowShowing)
        {
            qarrowImg.enabled = true;
            if (Input.GetButtonDown("Up"))
            {
                if (_currentArrow > 0)
                {
                    _currentArrow--;
                }
            }
            else if (Input.GetButtonDown("Down"))
            {
                if (_currentArrow < qarrowPoints.Length - 1)
                {
                    _currentArrow++;
                }
            }
            //update position of arrow
            qarrowImg.transform.position = qarrowPoints[_currentArrow].transform.position;
            //Select button
            if (Input.GetButtonDown("Select"))
            {
                switch (_currentArrow)
                {
                    case 0://Quit
                        if (gameObject.GetComponent<MainMenuScript>() != null)
                        {
                            gameObject.GetComponent<MainMenuScript>().QuitGame();
                            quitPanel.SetActive(false);
                        }
                        if (gameObject.GetComponent<LevelSelectScript>() != null) //IF I WANT TO PROMPT QUITTING FROM LEVEL SELECT
                        {
                            gameObject.GetComponent<Manager>().GotoMainMenu();
                            quitPanel.SetActive(false);
                        }
                        break;
                    case 1: //Dont Quit
                        NoButton();
                        break;

                }
            }
        }
    }
}
