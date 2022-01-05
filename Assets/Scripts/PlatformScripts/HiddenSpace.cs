using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Add to object with trigger to allow it to be invisible until the player enters area
 * 
 */

public class HiddenSpace : MonoBehaviour
{
       
    void OnTriggerStay2D(Collider2D other)
    {
        //Check if player is inside trigger
        if (other.gameObject.CompareTag("Player"))
        {
            //toggle off the renderer
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Check if player has left trigger
        if (other.gameObject.CompareTag("Player"))
        {
            //toggle on the renderer
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }
}
