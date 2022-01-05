using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Controls for shooting and also the Manager for shooting spells.
 * 
 */

public class PlayerShooting : MonoBehaviour
{
    //Reference to characterController
    public CharacterController controller;

    //Shooting Variables
    bool _shoot = false;
    bool _aimUp = false;
    bool _aimDown = false;
    bool _aimRight = false;
    bool _aimLeft = false;

    public bool fireType = true;
    public Image spellTypeImage;
    public Sprite[] spellSprites;


    public bool fireballScrollAquired = false;
    public bool freezeScrollAquired = false;


    private GameObject manager;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager");
        spellTypeImage.enabled = false;
    }

    //Input
    void Update()
    {
        if (manager.GetComponent<PauseScript>().gamePaused == false && manager.GetComponent<WinMenuScript>().gameWon == false)
        {
            if (Input.GetButtonDown("AimDown"))
            {
                _aimDown = true;
                _aimUp = false;
            }
            if (Input.GetButtonUp("AimDown"))
            {
                _aimDown = false;
            }
            if (Input.GetButtonDown("AimUp"))
            {
                _aimUp = true;
                _aimDown = false;
            }
            if (Input.GetButtonUp("AimUp"))
            {
                _aimUp = false;
            }

            if (Input.GetButtonDown("AimRight"))
            {
                _aimRight = true;
                _aimLeft = false;
            }
            if (Input.GetButtonUp("AimRight"))
            {
                _aimRight = false;
            }
            if (Input.GetButtonDown("AimLeft"))
            {
                _aimLeft = true;
                _aimRight = false;
            }
            if (Input.GetButtonUp("AimLeft"))
            {
                _aimLeft = false;
            }
            if (Input.GetButtonDown("Shoot"))
            {
                _shoot = true;
            }
            if (fireballScrollAquired)
            {
                spellTypeImage.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Tab) && freezeScrollAquired)
            {
                fireType = !fireType;
                if (fireType)
                {
                    spellTypeImage.sprite = spellSprites[0];
                }
                else if (!fireType)
                {
                    spellTypeImage.sprite = spellSprites[1];
                }
            }

        }


    }

    //Movement
    void FixedUpdate()
    {
        if (manager.GetComponent<PauseScript>().gamePaused == false && manager.GetComponent<WinMenuScript>().gameWon == false)
        {
            if (_shoot && (fireballScrollAquired && fireType) || _shoot && (freezeScrollAquired && !fireType))
            {
                if (manager.GetComponent<ManaScript>().CastMana(1))
                {
                    controller.Shoot(_aimUp, _aimDown, _aimRight, _aimLeft, fireType);
                }
                else
                {
                    Debug.Log("No Mana");
                }
                _shoot = false;
            }
            else if (_shoot)
            {
                _shoot = false;
            }
        }
    }
}
