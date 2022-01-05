using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Heart UI and heart/health functions
 * 
 */
public class HealthScript : MonoBehaviour
{
    //Reference to heartcontainer objects
    public GameObject[] heart;
    //Sprites full and empty heart containers
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;
    //Health of player
    private int _health = 3;
    //Is the player dead
    private bool _dead = false;
    //Max heart containers
    private const int MAXHEART = 5;
    //Current amount of heart containers
    public int heartContainers = 3;
    //Is the player invincible
    public bool invincible = false;
    //Amount of time the player is invincible for
    public float invincibleTime = 1f;
    //TImer to count invincibility
    private float _invTimer = 0f;
    //Player reference
    private GameObject _player;

    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        _health = heartContainers;
        //Get player reference
        _player = GameObject.FindGameObjectWithTag("Player");
        //Set heart containers to full/Active in scene
        for (int i = 0; i < heartContainers; i++)
        {
            heart[i].GetComponent<Image>().sprite = fullHeartSprite;
            heart[i].SetActive(true);
        }
    }
    //Function to take damage
    public void TakeDamage(int _dmg)
    {
        if (!invincible)
        {
            _health -= _dmg;
            HeartUI();
            Invincibility();
            //Kill player
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>().PlayOneShot(hitSound);
        }

    }
    //Function to give player invincibility
    public void Invincibility()
    {
        invincible = true;
        _player.GetComponent<SpriteRenderer>().color = Color.red;
    }
    //Heal player desired amount
    public void HealHealth(int _healAmount)
    {
        for (int i = 0; i < _healAmount; i++)
        {
            if (_health < heartContainers)
            {
                _health++;
            }
        }
        for (int i = 0; i < _health; i++)
        {
            heart[i].GetComponent<Image>().sprite = fullHeartSprite;
            heart[i].SetActive(true);
        }

    }

    //Add heart container to player current heart containers
    public void AddHeartContainer()
    {
        if (heartContainers < MAXHEART)
        {
            heartContainers++;
            HealHealth(heartContainers);
        }
        else
        {
            Debug.Log("At max health");
        }
    }
    //Update Heart UI if player takes damage
    public void HeartUI()
    {
        switch (_health)
        {
            case 5:
                heart[4].GetComponent<Image>().sprite = fullHeartSprite;
                break;
            case 4:
                heart[4].GetComponent<Image>().sprite = emptyHeartSprite;
                break;
            case 3:
                heart[3].GetComponent<Image>().sprite = emptyHeartSprite;
                heart[4].GetComponent<Image>().sprite = emptyHeartSprite;
                break;
            case 2:
                heart[4].GetComponent<Image>().sprite = emptyHeartSprite;
                heart[3].GetComponent<Image>().sprite = emptyHeartSprite;
                heart[2].GetComponent<Image>().sprite = emptyHeartSprite;
                break;
            case 1:

                heart[4].GetComponent<Image>().sprite = emptyHeartSprite;
                heart[3].GetComponent<Image>().sprite = emptyHeartSprite;
                heart[2].GetComponent<Image>().sprite = emptyHeartSprite;
                heart[1].GetComponent<Image>().sprite = emptyHeartSprite;
                break;
            case 0:

                heart[4].GetComponent<Image>().sprite = emptyHeartSprite;
                heart[3].GetComponent<Image>().sprite = emptyHeartSprite;
                heart[2].GetComponent<Image>().sprite = emptyHeartSprite;
                heart[1].GetComponent<Image>().sprite = emptyHeartSprite;
                heart[0].GetComponent<Image>().sprite = emptyHeartSprite;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Check if player is dead
        if (_health <= 0 && !_dead)
        {
            _dead = true;
            gameObject.GetComponent<DeathMenuScript>().DeathScreen();
        }
        //Check if player is invincible
        if (invincible)
        {
            _invTimer += Time.deltaTime;
            if (_invTimer >= invincibleTime)
            {
                invincible = false;
                _invTimer = 0;
                _player.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    public int GetPlayerHealth()
    {
        return _health;
    }
}
