using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Written by: Tyler McMillan
* Purpose of script:
* Finish line script: allows player to win levels
* 
*/
public class FinishLine : MonoBehaviour
{
    private GameObject _gameManager;
    [SerializeField] int _currentLvl;
    [SerializeField] AudioClip winSound;

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_gameManager.GetComponent<Score>().finishedLvl)
            {

                _gameManager.GetComponent<AudioSource>().PlayOneShot(winSound);
                _gameManager.GetComponent<Score>().finishedLvl = true;
                _gameManager.GetComponent<Score>().TimeScore();
                _gameManager.GetComponent<Score>().scoreKillEnemy();
                _gameManager.GetComponent<Score>().scorePlayerHealth();
                _gameManager.GetComponent<PlayerInfoScript>().lsCompleteLvl(_currentLvl, _gameManager.GetComponent<Score>().playerScore);
                _gameManager.GetComponent<PlayerInfoScript>().piSaveInfoToLevelSelect();
                _gameManager.GetComponent<WinMenuScript>().WinGame();
            }
        }
    }


}
