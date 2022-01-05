using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Written by: Tyler McMillan
* Purpose of script:
* Controls players input for movement / movement variables
* 
*/
public class PlayerMovment : MonoBehaviour
{
    //Reference to controller
    public CharacterController controller;
    //a/d key speed
    float _horizMove;
    //Player movement speed
    public float movementSpeed = 40f;
    //Are you crouching
    bool _crouch = false;

    //Jumping Variables
    bool _jump = false;
    //Are you pressing the jump key
    public bool pressingW = false;
    //How long have you currently jumped for
    float currentJumpTime = 0;
    //Length player is allowed to jump
    float maxJumpTime = 0.3f;

    //Player animation sprites
    public Sprite[] playerAnim;

    //Players crouch animations
    public Sprite[] playerCrouchAnim;
    public bool stillcrouching = false;
    [SerializeField] private AudioClip _movementSound;
    [SerializeField] private AudioClip _jumpSound;



    private GameObject manager;
    [SerializeField] private AudioSource WalkingAudio;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager");
    }
    //Input
    void Update()
    {
        if (manager.GetComponent<PauseScript>().gamePaused == false && manager.GetComponent<WinMenuScript>().gameWon == false)
        {
            _horizMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
            if (Input.GetButtonDown("Jump") && controller.m_Grounded && !manager.GetComponent<DeathMenuScript>().playerDead)
            {
                WalkingAudio.PlayOneShot(_jumpSound);
                _jump = true;
                pressingW = true;
                currentJumpTime = maxJumpTime;
            }
            if (Input.GetButtonUp("Jump"))
            {
                pressingW = false;
            }

            if (Input.GetButtonDown("Crouch"))
            {
                _crouch = true;
            }
            if (Input.GetButtonUp("Crouch"))
            {
                _crouch = false;
            }
        }
    }
    //Animation variables
    public float timebetweenAnim = 0.1f;
    private float _timer;
    private int _currentSprite = 0;
    private bool _reverse = false;
    public Sprite playerJumpSprite;
    //Movement
    void FixedUpdate()
    {
        if (manager.GetComponent<PauseScript>().gamePaused == false && manager.GetComponent<WinMenuScript>().gameWon == false)
        {

            //If moving update sprites
            if (_horizMove > 0.01 || _horizMove < -0.01)
            {
                _timer += Time.deltaTime;
                if (_timer > timebetweenAnim && !(_crouch || stillcrouching))
                {
                    if (_currentSprite < playerAnim.Length - 1 && _reverse == false)
                    {

                        _currentSprite++;
                    }
                    else if (_reverse == false)
                    {
                        _reverse = true;
                    }
                    else if (_currentSprite > 0 && _reverse == true) //REMOVE ELSE IF AND ADD A CURRENT++ IN ELSE IF BELOW IF YOU DONT WANT TO SLOW AT END OF ANIM
                    {
                        _currentSprite--;
                    }
                    else if (_reverse == true && _currentSprite == 0)
                    {
                        _reverse = false;
                        WalkingAudio.PlayOneShot(_movementSound);
                    }
                    _timer = 0;
                    gameObject.GetComponent<SpriteRenderer>().sprite = playerAnim[_currentSprite];
                }
                else if (_timer > timebetweenAnim && (_crouch || stillcrouching))
                {
                    if (_currentSprite < playerCrouchAnim.Length - 1 && _reverse == false)
                    {

                        _currentSprite++;
                    }
                    else if (_reverse == false)
                    {
                        _reverse = true;
                    }
                    else if (_currentSprite > 0 && _reverse == true) //REMOVE ELSE IF AND ADD A CURRENT++ IN ELSE IF BELOW IF YOU DONT WANT TO SLOW AT END OF ANIM
                    {
                        _currentSprite--;
                    }
                    else if (_reverse == true && _currentSprite == 0)
                    {
                        _reverse = false;
                        WalkingAudio.PlayOneShot(_movementSound);
                    }
                    _timer = 0;
                    gameObject.GetComponent<SpriteRenderer>().sprite = playerCrouchAnim[_currentSprite];
                }



            }
            //If not moving return back to old sprite & reset timer
            else
            {

                _currentSprite = 2;
                _timer = 0;
                if (!(_crouch || stillcrouching))
                {

                    gameObject.GetComponent<SpriteRenderer>().sprite = playerAnim[_currentSprite];
                }
                else if (_crouch || stillcrouching)
                {

                    gameObject.GetComponent<SpriteRenderer>().sprite = playerCrouchAnim[_currentSprite];
                }
            }

            controller.Move(_horizMove * Time.fixedDeltaTime, _crouch);







            if (_jump)
            {
                //if jumping set sprite to jump sprite
                gameObject.GetComponent<SpriteRenderer>().sprite = playerJumpSprite;
                _jump = controller.Jump(_jump);
            }
            //if not on ground switch to jump sprite
            else if (!controller.m_Grounded)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playerJumpSprite;
            }
            //Jump variables
            if (pressingW)
            {
                currentJumpTime = currentJumpTime - Time.fixedDeltaTime;
                controller.stillJump(currentJumpTime);
            }
            else if (!pressingW)
            {
                currentJumpTime = 0;
            }
        }
    }
}
