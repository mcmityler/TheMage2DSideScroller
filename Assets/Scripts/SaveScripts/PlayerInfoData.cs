using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Written by: Tyler McMillan
 * Decription: Purpose is saving the players info
 */
[System.Serializable]
public class PlayerInfoData
{
    //Coins
    public int piCoins = 0;
    public int piManaContainers = 4;
    public int piHeartContainers = 3;
    public bool piFireballScroll = false;
    public bool piFreezeScroll = false;

    public bool piManaContainer1 = false;
    public bool piManaContainer2 = false;
    public bool piManaContainer3 = false;

    public bool piHeartContainer1 = false;
    public bool piHeartContainer2 = false;



    //constructor data from player in variables
    public PlayerInfoData(PlayerInfoScript player)
    {
        piFireballScroll = player.gotFireballScroll;
        piFreezeScroll = player.gotFreezeScroll;
        piCoins = player.coins;
        piHeartContainers = player.heartContainers;
        piManaContainers = player.manaContainers;

        piManaContainer1 = player.ManaContainer1;
        piManaContainer2 = player.ManaContainer2;
        piManaContainer3 = player.ManaContainer3;

        piHeartContainer1 = player.HeartContainer1;
        piHeartContainer2 = player.HeartContainer2;


    }

}
