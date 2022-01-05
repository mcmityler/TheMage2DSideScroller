using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * 
 * Level Select Map Controls & Functions
 * 
 */
public class LevelSelectScript : MonoBehaviour
{
    //Player object
    [SerializeField] private GameObject player;
    //Waypoints for levels
    [SerializeField] private GameObject[] Waypoints;
    //Current level position on map/Where you want to be
    private int _lvlNum = 0;
    //Speed of player on map
    public float pSpeed = 2.0f;
    //Range to auto connect to spot
    public float playerRange = 40.0f;
    //What target is your current target (what you are moving towards rn)
    private int _currentTarg = 0;
    //Players total coins
    private int _totalCoin = 0;

    //Current level completetion
    private bool _lvl0Complete = false;
    private bool _lvl1Complete = false;
    private bool _lvl2Complete = false;
    private bool _lvl3Complete = false;
    private bool _lvl4Complete = false;
    private bool _lvl5Complete = false;
    private bool _lvl6Complete = false;
    private bool _lvl7Complete = false;
    private bool _lvl8Complete = false;

    //Lock sprite
    public Sprite lockImg;
    //Text box for coins
    public Text coinTxt;

    //GameObjects for tower/final boss
    [SerializeField] private GameObject towerUnfinishedImg;
    [SerializeField] private GameObject towerFinishedImg;

    //Text boxes for different levels
    [SerializeField] private Text[] HighScoreText;
    [SerializeField] private Text[] NewHighScoreText;
    [SerializeField] private Text[] ScoreText;
    //Scores for different levels
    private int _lvl0Score = 0;
    private int _lvl1Score = 0;
    private int _lvl2Score = 0;
    private int _lvl3Score = 0;
    private int _lvl4Score = 0;
    private int _lvl5Score = 0;
    private int _lvl6Score = 0;
    private int _lvl7Score = 0;
    private int _lvl8Score = 0;
    //What location just got a new highscore
    private int newHSLocation = 0;

    //Did it get a new highscore
    private bool _newHSlvl0 = false;
    private bool _newHSlvl1 = false;
    private bool _newHSlvl2 = false;
    private bool _newHSlvl3 = false;
    private bool _newHSlvl4 = false;
    private bool _newHSlvl5 = false;
    private bool _newHSlvl6 = false;
    private bool _newHSlvl7 = false;
    private bool _newHSlvl8 = false;

    private float _t = 0;
    private float _animTime = 1;
    public Sprite[] mplayerAnim;
    private int _animCount = 0;

    [SerializeField] private GameObject _map;
    private bool moveMapLeft = false;
    private bool moveMapRight = false;
    private bool moveMapRight2 = false;
    private int moveAmount = 500;
    private int moveCount = 0;
    private Vector3 mapLeftpos;
    private Vector3 mapRightpos;
    private Vector3 mapRightpos2;
    private int moveMapSpeed = 750;




    void Awake()
    {
        mapLeftpos = new Vector3(_map.transform.position.x, _map.transform.position.y, _map.transform.position.z);
        mapRightpos = new Vector3(_map.transform.position.x - 450, _map.transform.position.y, _map.transform.position.z);
        mapRightpos2 = new Vector3(_map.transform.position.x - 1500, _map.transform.position.y, _map.transform.position.z);


        //Load on launch of lvl select
        LoadLevelSelect();
        //Checks if levels complete and updates map images
        LevelCompletion();
        //Get coins text
        coinTxt.text = _totalCoin.ToString();
        player.GetComponent<Image>().sprite = mplayerAnim[_animCount];

        if (_lvlNum == 4)
        {
            _map.transform.position = mapRightpos;
        }
        if (_lvlNum > 4)
        {
            _map.transform.position = mapRightpos2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _t += Time.deltaTime;
        if (_t >= _animTime)
        {
            if (_animCount + 1 < mplayerAnim.Length)
            {
                _animCount++;
            }
            else
            {
                _animCount = 0;
            }

            player.GetComponent<Image>().sprite = mplayerAnim[_animCount];
            _t = 0;
        }
        if (moveMapRight && Vector3.Distance(_map.transform.position, mapRightpos2) > 10 && _lvlNum >= 5)
        {
            _map.transform.position = new Vector3(_map.transform.position.x - (moveMapSpeed * Time.deltaTime), _map.transform.position.y, _map.transform.position.z);
        }
        if (moveMapRight && Vector3.Distance(_map.transform.position, mapRightpos) > 2 && _lvlNum <= 4)
        {
            _map.transform.position = new Vector3(_map.transform.position.x - (moveMapSpeed * Time.deltaTime), _map.transform.position.y, _map.transform.position.z);
        }
        if (moveMapLeft && Vector3.Distance(_map.transform.position, mapRightpos) > 2 && _lvlNum >= 4)
        {
            _map.transform.position = new Vector3(_map.transform.position.x + (moveMapSpeed * Time.deltaTime), _map.transform.position.y, _map.transform.position.z);
        }
        if (moveMapLeft && Vector3.Distance(_map.transform.position, mapLeftpos) > 2 && _lvlNum <= 3)
        {
            _map.transform.position = new Vector3(_map.transform.position.x + (moveMapSpeed * Time.deltaTime), _map.transform.position.y, _map.transform.position.z);
        }

        //Display Scores above current level
        DisplayScore();
        //Controls to navigate level select menu
        LevelSelectControls();

        //Check if you are close enough to target to go to next 
        if (_currentTarg < _lvlNum && Mathf.Abs(Vector3.Distance(player.transform.position, Waypoints[_currentTarg].transform.position)) < playerRange + 20)
        {
            _currentTarg++;
        }
        if (_currentTarg > _lvlNum && Mathf.Abs(Vector3.Distance(player.transform.position, Waypoints[_currentTarg].transform.position)) < playerRange + 20)
        {
            _currentTarg--;
        }
        //Snap to target if close enough 
        if (Mathf.Abs(Vector3.Distance(player.transform.position, Waypoints[_currentTarg].transform.position)) < playerRange + 2)
        {
            player.transform.position = Waypoints[_currentTarg].transform.position;
        }
        //Move if not close to target
        else if (player.transform.position != Waypoints[_currentTarg].transform.position)
        {
            player.transform.position += ((Waypoints[_currentTarg].transform.position - player.transform.position) * Time.deltaTime * pSpeed);

        }
    }
    //Controls to navigate 
    void LevelSelectControls()
    {
        //Check if to display highscore or if you moved/started level
        if ((_lvlNum != newHSLocation) || (Input.GetButtonDown("Select")))
        {
            _newHSlvl0 = false;
            _newHSlvl1 = false;
            _newHSlvl2 = false;
            _newHSlvl3 = false;
            _newHSlvl4 = false;
            _newHSlvl5 = false;
            _newHSlvl6 = false;
            _newHSlvl7 = false;
            _newHSlvl8 = false;
        }
        if (Input.GetButtonDown("Up"))
        {
            if (_lvlNum == 0 && _lvl0Complete)
            {
                _lvlNum = 1;
            }
            if (_lvlNum == 4)
            {
                moveMapLeft = true;
                moveMapRight = false;
                moveMapRight2 = false;
                _lvlNum = 3;
            }
            if (_lvlNum == 7)
            {
                _lvlNum = 6;
            }
        }
        if (Input.GetButtonDown("Left"))
        {

            if (_lvlNum == 2)
            {
                _lvlNum = 1;
            }
            else if (_lvlNum == 3)
            {
                _lvlNum = 2;
            }
            if (_lvlNum == 6)
            {
                _lvlNum = 5;
            }
            else if (_lvlNum == 5)
            {
                _lvlNum = 4;
                moveMapRight = false;
                moveMapRight2 = false;
                moveMapLeft = true;
            }
            if (_lvlNum == 7 && _lvl7Complete)
            {
                _lvlNum = 8;
            }
        }
        if (Input.GetButtonDown("Down"))
        {

            if (_lvlNum == 1)
            {
                _lvlNum = 0;
            }
            if (_lvlNum == 3 && _lvl3Complete)
            {
                _lvlNum = 4;
                moveMapRight = true;
                moveMapRight2 = false;
                moveMapLeft = false;
            }
            if (_lvlNum == 5)
            {

                _lvlNum = 4;
                moveMapRight = false;
                moveMapRight2 = false;
                moveMapLeft = true;
            }
            if (_lvlNum == 6 && _lvl6Complete)
            {
                _lvlNum = 7;
            }
        }
        if (Input.GetButtonDown("Right"))
        {
            if (_lvlNum == 1 && _lvl1Complete)
            {
                _lvlNum = 2;
            }
            else if (_lvlNum == 2 && _lvl2Complete)
            {
                _lvlNum = 3;
            }
            if (_lvlNum == 4 && _lvl4Complete)
            {

                _lvlNum = 5;

                moveMapRight = true;
                moveMapRight2 = true;
                moveMapLeft = false;

            }
            else if (_lvlNum == 5 && _lvl5Complete)
            {
                _lvlNum = 6;
            }
            if (_lvlNum == 8)
            {
                _lvlNum = 7;
            }
        }
        if (Input.GetButtonDown("Select"))
        {
            switch (_lvlNum)
            {
                case 0:
                    SaveLevelSelect();
                    SceneManager.LoadScene("Prototype1");
                    break;
                case 1:
                    SaveLevelSelect();
                    SceneManager.LoadScene("Level1");
                    break;
                case 2:
                    SaveLevelSelect();
                    SceneManager.LoadScene("Level2");
                    break;
                case 3:
                    SaveLevelSelect();
                    SceneManager.LoadScene("Level3");
                    break;
                case 4:
                    SaveLevelSelect();
                    SceneManager.LoadScene("Level4");
                    break;
                case 5:
                    SaveLevelSelect();
                    SceneManager.LoadScene("Level5");
                    break;
                case 6:
                    SaveLevelSelect();
                    SceneManager.LoadScene("Level6");
                    break;
                case 7:
                    SaveLevelSelect();
                    SceneManager.LoadScene("Level7");
                    break;
                case 8:
                    SaveLevelSelect();
                    SceneManager.LoadScene("Level8");
                    break;
                Default:
                    Debug.Log("No Scene to load");
                    break;
            }
        }
        //Go to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveLevelSelect();
            SceneManager.LoadScene("MainMenu");
        }
    }

    //Check if level has been completed and update image
    void LevelCompletion()
    {
        if (_lvl0Complete)
        {
            Waypoints[0].GetComponent<Image>().color = Color.green;
            Waypoints[1].GetComponent<Image>().sprite = null;
        }
        if (_lvl1Complete)
        {
            Waypoints[1].GetComponent<Image>().color = Color.green;
            Waypoints[2].GetComponent<Image>().sprite = null;
        }
        if (_lvl2Complete)
        {
            Waypoints[2].GetComponent<Image>().color = Color.green;
            Waypoints[3].GetComponent<Image>().sprite = null;
        }
        if (_lvl3Complete)
        {
            Waypoints[3].GetComponent<Image>().color = Color.green;
            Waypoints[4].GetComponent<Image>().sprite = null;
        }
        if (_lvl4Complete)
        {
            Waypoints[4].GetComponent<Image>().color = Color.green;
            Waypoints[5].GetComponent<Image>().sprite = null;
        }
        if (_lvl5Complete)
        {
            Waypoints[5].GetComponent<Image>().color = Color.green;
            Waypoints[6].GetComponent<Image>().sprite = null;
        }
        if (_lvl6Complete)
        {
            Waypoints[6].GetComponent<Image>().color = Color.green;
            Waypoints[7].GetComponent<Image>().sprite = null;
        }
        if (_lvl7Complete)
        {
            Waypoints[7].GetComponent<Image>().color = Color.green;
            Waypoints[8].GetComponent<Image>().sprite = null;
        }
        if (_lvl8Complete)
        {
            Waypoints[8].GetComponent<Image>().color = Color.green;
            towerFinishedImg.GetComponent<Image>().enabled = true;
            towerUnfinishedImg.GetComponent<Image>().enabled = false;
        }
        if (!_lvl0Complete)
        {
            Waypoints[1].GetComponent<Image>().sprite = lockImg;
            Waypoints[1].GetComponent<Image>().color = Color.white;
        }
        if (!_lvl1Complete)
        {
            Waypoints[2].GetComponent<Image>().sprite = lockImg;
            Waypoints[2].GetComponent<Image>().color = Color.white;
        }
        if (!_lvl2Complete)
        {
            Waypoints[3].GetComponent<Image>().sprite = lockImg;
            Waypoints[3].GetComponent<Image>().color = Color.white;
        }
        if (!_lvl3Complete)
        {
            Waypoints[4].GetComponent<Image>().sprite = lockImg;
            Waypoints[4].GetComponent<Image>().color = Color.white;
        }
        if (!_lvl4Complete)
        {
            Waypoints[5].GetComponent<Image>().sprite = lockImg;
            Waypoints[5].GetComponent<Image>().color = Color.white;
        }
        if (!_lvl5Complete)
        {
            Waypoints[6].GetComponent<Image>().sprite = lockImg;
            Waypoints[6].GetComponent<Image>().color = Color.white;
        }
        if (!_lvl6Complete)
        {
            Waypoints[7].GetComponent<Image>().sprite = lockImg;
            Waypoints[7].GetComponent<Image>().color = Color.white;
        }
        if (!_lvl7Complete)
        {
            Waypoints[8].GetComponent<Image>().sprite = lockImg;
            Waypoints[8].GetComponent<Image>().color = Color.white;
        }
        if (!_lvl8Complete)
        {
            towerFinishedImg.GetComponent<Image>().enabled = false;
            towerUnfinishedImg.GetComponent<Image>().enabled = true;
        }
    }
    //Load level Select from player info
    public void LoadLevelSelect()
    {
        //load ls info from save file
        gameObject.GetComponent<PlayerInfoScript>().lsLoadInfo();
        //Load Level Select from player info script
        _lvl0Complete = gameObject.GetComponent<PlayerInfoScript>().lvl0CompleteData;
        _lvl1Complete = gameObject.GetComponent<PlayerInfoScript>().lvl1CompleteData;
        _lvl2Complete = gameObject.GetComponent<PlayerInfoScript>().lvl2CompleteData;
        _lvl3Complete = gameObject.GetComponent<PlayerInfoScript>().lvl3CompleteData;
        _lvl4Complete = gameObject.GetComponent<PlayerInfoScript>().lvl4CompleteData;
        _lvl5Complete = gameObject.GetComponent<PlayerInfoScript>().lvl5CompleteData;
        _lvl6Complete = gameObject.GetComponent<PlayerInfoScript>().lvl6CompleteData;
        _lvl7Complete = gameObject.GetComponent<PlayerInfoScript>().lvl7CompleteData;
        _lvl8Complete = gameObject.GetComponent<PlayerInfoScript>().lvl8CompleteData;
        _lvlNum = gameObject.GetComponent<PlayerInfoScript>().lsLocation;
        _totalCoin = gameObject.GetComponent<PlayerInfoScript>().coinTotal;
        _lvl0Score = gameObject.GetComponent<PlayerInfoScript>().lvl0Score;
        _lvl1Score = gameObject.GetComponent<PlayerInfoScript>().lvl1Score;
        _lvl2Score = gameObject.GetComponent<PlayerInfoScript>().lvl2Score;
        _lvl3Score = gameObject.GetComponent<PlayerInfoScript>().lvl3Score;
        _lvl4Score = gameObject.GetComponent<PlayerInfoScript>().lvl4Score;
        _lvl5Score = gameObject.GetComponent<PlayerInfoScript>().lvl5Score;
        _lvl6Score = gameObject.GetComponent<PlayerInfoScript>().lvl6Score;
        _lvl7Score = gameObject.GetComponent<PlayerInfoScript>().lvl7Score;
        _lvl8Score = gameObject.GetComponent<PlayerInfoScript>().lvl8Score;
        _newHSlvl0 = gameObject.GetComponent<PlayerInfoScript>().lvl0NewHSData;
        _newHSlvl1 = gameObject.GetComponent<PlayerInfoScript>().lvl1NewHSData;
        _newHSlvl2 = gameObject.GetComponent<PlayerInfoScript>().lvl2NewHSData;
        _newHSlvl3 = gameObject.GetComponent<PlayerInfoScript>().lvl3NewHSData;
        _newHSlvl4 = gameObject.GetComponent<PlayerInfoScript>().lvl4NewHSData;
        _newHSlvl5 = gameObject.GetComponent<PlayerInfoScript>().lvl5NewHSData;
        _newHSlvl6 = gameObject.GetComponent<PlayerInfoScript>().lvl6NewHSData;
        _newHSlvl7 = gameObject.GetComponent<PlayerInfoScript>().lvl7NewHSData;
        _newHSlvl8 = gameObject.GetComponent<PlayerInfoScript>().lvl8NewHSData;

        //update current position
        _currentTarg = _lvlNum;
        //Update players position
        player.transform.position = Waypoints[_lvlNum].transform.position;

    }
    //Save level select to player info
    public void SaveLevelSelect()
    {
        //Set values to save in player info script
        gameObject.GetComponent<PlayerInfoScript>().lvl0CompleteData = _lvl0Complete;
        gameObject.GetComponent<PlayerInfoScript>().lvl1CompleteData = _lvl1Complete;
        gameObject.GetComponent<PlayerInfoScript>().lvl2CompleteData = _lvl2Complete;
        gameObject.GetComponent<PlayerInfoScript>().lvl3CompleteData = _lvl3Complete;
        gameObject.GetComponent<PlayerInfoScript>().lvl4CompleteData = _lvl4Complete;
        gameObject.GetComponent<PlayerInfoScript>().lvl5CompleteData = _lvl5Complete;
        gameObject.GetComponent<PlayerInfoScript>().lvl6CompleteData = _lvl6Complete;
        gameObject.GetComponent<PlayerInfoScript>().lvl7CompleteData = _lvl7Complete;
        gameObject.GetComponent<PlayerInfoScript>().lvl8CompleteData = _lvl8Complete;

        gameObject.GetComponent<PlayerInfoScript>().lsLocation = _lvlNum;


        gameObject.GetComponent<PlayerInfoScript>().coinTotal = _totalCoin;

        gameObject.GetComponent<PlayerInfoScript>().lvl0Score = _lvl0Score;
        gameObject.GetComponent<PlayerInfoScript>().lvl1Score = _lvl1Score;
        gameObject.GetComponent<PlayerInfoScript>().lvl2Score = _lvl2Score;
        gameObject.GetComponent<PlayerInfoScript>().lvl3Score = _lvl3Score;
        gameObject.GetComponent<PlayerInfoScript>().lvl4Score = _lvl4Score;
        gameObject.GetComponent<PlayerInfoScript>().lvl5Score = _lvl5Score;
        gameObject.GetComponent<PlayerInfoScript>().lvl6Score = _lvl6Score;
        gameObject.GetComponent<PlayerInfoScript>().lvl7Score = _lvl7Score;
        gameObject.GetComponent<PlayerInfoScript>().lvl8Score = _lvl8Score;

        gameObject.GetComponent<PlayerInfoScript>().lvl0NewHSData = _newHSlvl0;
        gameObject.GetComponent<PlayerInfoScript>().lvl1NewHSData = _newHSlvl1;
        gameObject.GetComponent<PlayerInfoScript>().lvl2NewHSData = _newHSlvl2;
        gameObject.GetComponent<PlayerInfoScript>().lvl3NewHSData = _newHSlvl3;
        gameObject.GetComponent<PlayerInfoScript>().lvl4NewHSData = _newHSlvl4;
        gameObject.GetComponent<PlayerInfoScript>().lvl5NewHSData = _newHSlvl5;
        gameObject.GetComponent<PlayerInfoScript>().lvl6NewHSData = _newHSlvl6;
        gameObject.GetComponent<PlayerInfoScript>().lvl7NewHSData = _newHSlvl7;
        gameObject.GetComponent<PlayerInfoScript>().lvl8NewHSData = _newHSlvl8;

        //Save info into file
        gameObject.GetComponent<PlayerInfoScript>().lsSaveInfo();
    }
    //Display score on level select screen above individual levels
    public void DisplayScore()
    {

        //Set all text boxes to disabled
        for (int i = 0; i < ScoreText.Length; i++)
        {
            ScoreText[i].enabled = false;
            HighScoreText[i].enabled = false;
            NewHighScoreText[i].enabled = false;
        }
        //Check if new highscore
        if (_newHSlvl0)
        {
            newHSLocation = 0;
        }
        if (_newHSlvl1)
        {
            newHSLocation = 1;
        }
        if (_newHSlvl2)
        {
            newHSLocation = 2;
        }
        if (_newHSlvl3)
        {
            newHSLocation = 3;
        }
        if (_newHSlvl4)
        {
            newHSLocation = 4;
        }
        if (_newHSlvl5)
        {
            newHSLocation = 5;
        }
        if (_newHSlvl6)
        {
            newHSLocation = 6;
        }
        if (_newHSlvl7)
        {
            newHSLocation = 7;
        }
        if (_newHSlvl8)
        {
            newHSLocation = 8;
        }
        if (_newHSlvl0 || _newHSlvl1 || _newHSlvl2 || _newHSlvl3 || _newHSlvl4 || _newHSlvl5 || _newHSlvl6 || _newHSlvl7 || _newHSlvl8)
        {
            NewHighScoreText[newHSLocation].enabled = true;
        }
        //Check what level you are on to display its score
        switch (_lvlNum)
        {
            case 0:
                HighScoreText[_lvlNum].text = _lvl0Score.ToString();
                break;
            case 1:
                HighScoreText[_lvlNum].text = _lvl1Score.ToString();

                break;
            case 2:
                HighScoreText[_lvlNum].text = _lvl2Score.ToString();
                break;
            case 3:
                HighScoreText[_lvlNum].text = _lvl3Score.ToString();
                break;
            case 4:
                HighScoreText[_lvlNum].text = _lvl4Score.ToString();
                break;
            case 5:
                HighScoreText[_lvlNum].text = _lvl5Score.ToString();
                break;
            case 6:
                HighScoreText[_lvlNum].text = _lvl6Score.ToString();
                break;
            case 7:
                HighScoreText[_lvlNum].text = _lvl7Score.ToString();
                break;
            case 8:
                HighScoreText[_lvlNum].text = _lvl8Score.ToString();
                break;

        }
        HighScoreText[_lvlNum].enabled = true;
        ScoreText[_lvlNum].enabled = true;


    }

}
