using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * 
 * ACTUAL SAVE INFO(SAVES TO FILE AND LOADS FROM FILE)
 * 
 */
public class PlayerInfoScript : MonoBehaviour
{


    //LEVEL SELECT INFORMATION -- LEVEL SELECT INFO --
    //Have the levels been completed 
    public bool lvl0CompleteData = false;
    public bool lvl1CompleteData = false;
    public bool lvl2CompleteData = false;
    public bool lvl3CompleteData = false;
    public bool lvl4CompleteData = false;
    public bool lvl5CompleteData = false;
    public bool lvl6CompleteData = false;
    public bool lvl7CompleteData = false;
    public bool lvl8CompleteData = false;

    //Location of player on level select
    public int lsLocation = 0;

    //Total Coin amount
    public int coinTotal = 0;

    //Score for each level
    public int lvl0Score = 0;
    public int lvl1Score = 0;
    public int lvl2Score = 0;
    public int lvl3Score = 0;
    public int lvl4Score = 0;
    public int lvl5Score = 0;
    public int lvl6Score = 0;
    public int lvl7Score = 0;
    public int lvl8Score = 0;

    //Is there a new high score
    public bool lvl0NewHSData = false;
    public bool lvl1NewHSData = false;
    public bool lvl2NewHSData = false;
    public bool lvl3NewHSData = false;
    public bool lvl4NewHSData = false;
    public bool lvl5NewHSData = false;
    public bool lvl6NewHSData = false;
    public bool lvl7NewHSData = false;
    public bool lvl8NewHSData = false;


    //PLAYER INFO --- PLAYER INFORMATION -----
    public int heartContainers = 3;
    public int manaContainers = 4;
    public bool gotFireballScroll = false;
    public bool gotFreezeScroll = false;
    public bool ManaContainer1 = false;
    public bool ManaContainer2 = false;
    public bool ManaContainer3 = false;

    public bool HeartContainer1 = false;
    public bool HeartContainer2 = false;
    //Amount of Coins Collected
    public int coins = 0;

    //---------Save & Load --------

    //Save players Info
    public void piSaveInfo()
    {
        Debug.Log("Saved Players info");
        PlayerInfoSaveScript.SavePI(this);

    }
    //Load Players Info
    public void piLoadInfo()
    {

        GameObject _p = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Load Players info");
        PlayerInfoData data = PlayerInfoSaveScript.LoadPI();
        if (data != null)
        {
            heartContainers = data.piHeartContainers;
            HeartContainer1 = data.piHeartContainer1;
            HeartContainer2 = data.piHeartContainer2;
            ManaContainer1 = data.piManaContainer1;
            ManaContainer2 = data.piManaContainer2;
            ManaContainer3 = data.piManaContainer3;
            manaContainers = data.piManaContainers;
            gotFireballScroll = data.piFireballScroll;
            gotFreezeScroll = data.piFreezeScroll;


        }
        gameObject.GetComponent<HealthScript>().heartContainers = heartContainers;
        gameObject.GetComponent<ManaScript>().loadMana(manaContainers);
        _p.GetComponent<PlayerShooting>().fireballScrollAquired = gotFireballScroll;
        _p.GetComponent<PlayerShooting>().freezeScrollAquired = gotFreezeScroll;
    }
    public void piSaveInfoToLevelSelect()
    {

        GameObject _p = GameObject.FindGameObjectWithTag("Player");
        heartContainers = gameObject.GetComponent<HealthScript>().heartContainers;
        manaContainers = gameObject.GetComponent<ManaScript>().manapool;
        gotFireballScroll = _p.GetComponent<PlayerShooting>().fireballScrollAquired;
        gotFreezeScroll = _p.GetComponent<PlayerShooting>().freezeScrollAquired;
        piSaveInfo();
    }

    //Reset player info on starting a new game
    public void resetSaveInfo()
    {
        Debug.Log("Reset Save");
        ManaContainer1 = false;
        ManaContainer2 = false;
        ManaContainer3 = false;
        HeartContainer1 = false;
        HeartContainer2 = false;
        gotFireballScroll = false;
        gotFreezeScroll = false;
        heartContainers = 3;
        manaContainers = 4;
        coins = 0;
        lvl0CompleteData = false;
        lvl1CompleteData = false;
        lvl2CompleteData = false;
        lvl3CompleteData = false;
        lvl4CompleteData = false;
        lvl5CompleteData = false;
        lvl6CompleteData = false;
        lvl7CompleteData = false;
        lvl8CompleteData = false;
        lvl0Score = 0;
        lvl1Score = 0;
        lvl2Score = 0;
        lvl3Score = 0;
        lvl4Score = 0;
        lvl5Score = 0;
        lvl6Score = 0;
        lvl7Score = 0;
        lvl8Score = 0;
        lvl0NewHSData = false;
        lvl1NewHSData = false;
        lvl2NewHSData = false;
        lvl3NewHSData = false;
        lvl4NewHSData = false;
        lvl5NewHSData = false;
        lvl6NewHSData = false;
        lvl7NewHSData = false;
        lvl8NewHSData = false;
        lsLocation = 0;
        PlayerInfoSaveScript.SavePI(this);
        LevelSelectSave.SaveLS(this);

    }

    public void lsSaveInfo()
    {

        Debug.Log("Saved Level info");
        LevelSelectSave.SaveLS(this);

    }
    //Load Players Info
    public void lsLoadInfo()
    {
        Debug.Log("Load Level info");
        LevelSelectData data = LevelSelectSave.LoadLS();
        if (data != null)
        {
            Debug.Log(data.saveLvl0Complete);
            lvl0CompleteData = data.saveLvl0Complete;
            lvl1CompleteData = data.saveLvl1Complete;
            lvl2CompleteData = data.saveLvl2Complete;
            lvl3CompleteData = data.saveLvl3Complete;
            lvl4CompleteData = data.saveLvl4Complete;
            lvl5CompleteData = data.saveLvl5Complete;
            lvl6CompleteData = data.saveLvl6Complete;
            lvl7CompleteData = data.saveLvl7Complete;
            lvl8CompleteData = data.saveLvl8Complete;
            lvl0Score = data.savelsLvl0Score;
            lvl1Score = data.savelsLvl1Score;
            lvl2Score = data.savelsLvl2Score;
            lvl3Score = data.savelsLvl3Score;
            lvl4Score = data.savelsLvl4Score;
            lvl5Score = data.savelsLvl5Score;
            lvl6Score = data.savelsLvl6Score;
            lvl7Score = data.savelsLvl7Score;
            lvl8Score = data.savelsLvl8Score;
            lvl0NewHSData = data.savelsNewHSlvl0;
            lvl1NewHSData = data.savelsNewHSlvl1;
            lvl2NewHSData = data.savelsNewHSlvl2;
            lvl3NewHSData = data.savelsNewHSlvl3;
            lvl4NewHSData = data.savelsNewHSlvl4;
            lvl5NewHSData = data.savelsNewHSlvl5;
            lvl6NewHSData = data.savelsNewHSlvl6;
            lvl7NewHSData = data.savelsNewHSlvl7;
            lvl8NewHSData = data.savelsNewHSlvl8;
            lsLocation = data.savelsLocation;
            coinTotal = data.savelsCoinTotal;
        }
    }

    //Complete each level
    public void lsCompleteLvl(int _lvlNum, int _newScore)
    {


        lsLoadInfo();
        switch (_lvlNum)
        {
            case 0:
                Debug.Log("set lvl0 true");
                lvl0CompleteData = true;
                FinishLevel(_lvlNum, _newScore);
                break;
            case 1:
                lvl1CompleteData = true;
                FinishLevel(_lvlNum, _newScore);
                break;
            case 2:
                lvl2CompleteData = true;
                FinishLevel(_lvlNum, _newScore);
                break;
            case 3:
                lvl3CompleteData = true;
                FinishLevel(_lvlNum, _newScore);
                break;
            case 4:
                lvl4CompleteData = true;
                FinishLevel(_lvlNum, _newScore);
                break;
            case 5:
                lvl5CompleteData = true;
                FinishLevel(_lvlNum, _newScore);
                break;
            case 6:
                lvl6CompleteData = true;
                FinishLevel(_lvlNum, _newScore);
                break;
            case 7:
                lvl7CompleteData = true;
                FinishLevel(_lvlNum, _newScore);
                break;
            case 8:
                lvl8CompleteData = true;
                FinishLevel(_lvlNum, _newScore);
                break;
            default:
                Debug.Log("No lvl to complete");
                break;
        }

    }
    //Finish level function
    void FinishLevel(int _lvlNum, int _newScore)
    {
        TotalCoins();
        NewHighScore(_lvlNum, _newScore);
        lsSaveInfo();
    }
    //Set/Check if new highscore for level
    void NewHighScore(int _lvl, int _newScore)
    {
        switch (_lvl)
        {
            case 0:
                if (_newScore > lvl0Score)
                {
                    lvl0Score = _newScore;
                    lvl0NewHSData = true;
                }
                break;
            case 1:
                if (_newScore > lvl1Score)
                {
                    lvl1Score = _newScore;
                    lvl1NewHSData = true;
                }
                break;
            case 2:
                if (_newScore > lvl2Score)
                {
                    lvl2Score = _newScore;
                    lvl2NewHSData = true;
                }
                break;
            case 3:
                if (_newScore > lvl3Score)
                {
                    lvl3Score = _newScore;
                    lvl3NewHSData = true;
                }
                break;
            case 4:
                if (_newScore > lvl4Score)
                {
                    lvl4Score = _newScore;
                    lvl4NewHSData = true;
                }
                break;
            case 5:
                if (_newScore > lvl5Score)
                {
                    lvl5Score = _newScore;
                    lvl5NewHSData = true;
                }
                break;
            case 6:
                if (_newScore > lvl6Score)
                {
                    lvl6Score = _newScore;
                    lvl6NewHSData = true;
                }
                break;
            case 7:
                if (_newScore > lvl7Score)
                {
                    lvl7Score = _newScore;
                    lvl7NewHSData = true;
                }
                break;
            case 8:
                if (_newScore > lvl8Score)
                {
                    lvl8Score = _newScore;
                    lvl8NewHSData = true;
                }
                break;
            default:
                Debug.Log("No new highscore");
                break;
        }


    }
    //Total up the coins
    public void TotalCoins()
    {
        coinTotal += coins;
    }
}
