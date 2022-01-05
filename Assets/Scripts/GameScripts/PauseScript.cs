using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Pause Menu Script
 * Script toggles the paused state.
 */
public class PauseScript : MonoBehaviour
{
    public bool gamePaused = false;
    public Text pauseText;
    public GameObject panel;

   

    public void Paused()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            Time.timeScale = 0f;
            panel.SetActive(true);
        }
        if (!gamePaused)
        {
            Time.timeScale = 1f;
            panel.SetActive(false);
        }
    }
}
