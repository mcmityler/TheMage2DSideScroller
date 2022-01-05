using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Platform that can be destroyed by a players projectile. Also if it has a hidden space it will reveal it completely unless toggled off
 * or if you dont want it to be destroyed add the HiddenSpace script onto area you want to hide until player enters. 
 * 
 */
public class DestroyablePlatform : MonoBehaviour
{
    public GameObject hiddenSpace;
    public bool destroyhidden = true;
   
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            if(hiddenSpace!= null && destroyhidden)
            {
                Destroy(hiddenSpace);
            }
            
        }
    }
}
