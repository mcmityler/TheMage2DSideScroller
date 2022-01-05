using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Camera Manager: Controls camera position.
 * 
 */
public class CameraManager : MonoBehaviour
{
    //Player gameobject
    private GameObject _player;
    //Clamp positions
    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); //Get reference
    }

    void LateUpdate()
    {
        float _x = Mathf.Clamp(_player.transform.position.x, xMin, xMax); //Clamp camera to player/dont go past edge positions
        float _y = Mathf.Clamp(_player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(_x, _y, gameObject.transform.position.z); //Set new position of camera.
    }
}
