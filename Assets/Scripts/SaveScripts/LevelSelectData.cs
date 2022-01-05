using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Written by: Tyler McMillan
 * Decription: Purpose is saving the players info
 */
[System.Serializable]
public class LevelSelectData
{
    public bool saveLvl0Complete;
    public bool saveLvl1Complete;
    public bool saveLvl2Complete;
    public bool saveLvl3Complete;
    public bool saveLvl4Complete;
    public bool saveLvl5Complete;
    public bool saveLvl6Complete;
    public bool saveLvl7Complete;
    public bool saveLvl8Complete;
    public int savelsLocation;

    public int savelsLvl0Score;
    public int savelsLvl1Score;
    public int savelsLvl2Score;
    public int savelsLvl3Score;
    public int savelsLvl4Score;
    public int savelsLvl5Score;
    public int savelsLvl6Score;
    public int savelsLvl7Score;
    public int savelsLvl8Score;

    public bool savelsNewHSlvl0;
    public bool savelsNewHSlvl1;
    public bool savelsNewHSlvl2;
    public bool savelsNewHSlvl3;
    public bool savelsNewHSlvl4;
    public bool savelsNewHSlvl5;
    public bool savelsNewHSlvl6;
    public bool savelsNewHSlvl7;
    public bool savelsNewHSlvl8;

    public int savelsCoinTotal;

    //constructor data from player in variables
    public LevelSelectData(PlayerInfoScript player)
    {

        saveLvl0Complete = player.lvl0CompleteData;
        saveLvl1Complete = player.lvl1CompleteData;
        saveLvl2Complete = player.lvl2CompleteData;
        saveLvl3Complete = player.lvl3CompleteData;
        saveLvl4Complete = player.lvl4CompleteData;
        saveLvl5Complete = player.lvl5CompleteData;
        saveLvl6Complete = player.lvl6CompleteData;
        saveLvl7Complete = player.lvl7CompleteData;
        saveLvl8Complete = player.lvl8CompleteData;

        savelsLvl0Score = player.lvl0Score;
        savelsLvl1Score = player.lvl1Score;
        savelsLvl2Score = player.lvl2Score;
        savelsLvl3Score = player.lvl3Score;
        savelsLvl4Score = player.lvl4Score;
        savelsLvl5Score = player.lvl5Score;
        savelsLvl6Score = player.lvl6Score;
        savelsLvl7Score = player.lvl7Score;
        savelsLvl8Score = player.lvl8Score;

        savelsNewHSlvl0 = player.lvl0NewHSData;
        savelsNewHSlvl1 = player.lvl1NewHSData;
        savelsNewHSlvl2 = player.lvl2NewHSData;
        savelsNewHSlvl3 = player.lvl3NewHSData;
        savelsNewHSlvl4 = player.lvl4NewHSData;
        savelsNewHSlvl5 = player.lvl5NewHSData;
        savelsNewHSlvl6 = player.lvl6NewHSData;
        savelsNewHSlvl7 = player.lvl7NewHSData;
        savelsNewHSlvl8 = player.lvl8NewHSData;

        savelsLocation = player.lsLocation;

        savelsCoinTotal = player.coinTotal;






    }

}
