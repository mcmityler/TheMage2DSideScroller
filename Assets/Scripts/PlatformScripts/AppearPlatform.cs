using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Platform block that player can shoot and have appear
 * for a desired amount of time
 * 
 */

public class AppearPlatform : MonoBehaviour
{
    //Has this platform been hit by a projectile
    private bool _hit = false;
    //Counter for how long it has been hit for (counts down)
    private float _hitTime = 0f;
    //How long should the platform appear for
    public float appearLength = 3.0f;

    //Sprite images for the platofrm
    public Sprite outlineImg;
    public Sprite fullImg;

    // Update is called once per frame
    void Update()
    {
        //check if its been hit
        if (_hit)
        {
            //Still has time left
            if(_hitTime > 0)
            {
                //Remove deltat time
                _hitTime -= Time.deltaTime;
            }
            else if (_hitTime <= 0) //Check if the time has passed
            {
                _hit = false; //set hit to false
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true; //return the collider back to a trigger
                gameObject.layer = 11;//outlineImg layer (so crouch works)
                gameObject.tag = "OutlineBlock"; //Outlineblock tag
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = outlineImg; //Change image of sprite
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check if it has been hit by the fireball
        if (other.gameObject.CompareTag("Fireball"))
        {
            //destroy fireball
            Destroy(other.gameObject);
            //Set trigger to collider
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.layer = 0;//default layer
            gameObject.tag = "Ground";//ground tag
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = fullImg; //Change image of sprite
            _hit = true; //Been hit by projectile
            _hitTime = appearLength; //Set the hittime back to the max time it appears for.
        }
    }
}
