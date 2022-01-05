using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Destroys spell after desired time
 * 
 */
public class DestroyScript : MonoBehaviour
{
    [SerializeField] private float destroyTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        // Kills the game object in desired seconds after loading the object
        Destroy(gameObject, destroyTime);
    }

}
