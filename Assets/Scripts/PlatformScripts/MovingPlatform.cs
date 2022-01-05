using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Written by: Tyler McMillan
 * Purpose of script:
 * Platform that moves, want 3 different options
 * - translation , goes straight to spot same speed and back
 * - Lerp, goes to spot but goes the fastest when furthest away and slowly reaches when closer
 * - Move only if player is on it, if player gets off platform it stops moving / changes colour when player jumps on it
 * 
 */

public class MovingPlatform : MonoBehaviour
{
    //Is the platofrm going to do a translation or a LERP
    [SerializeField] bool lerp = false;
    public GameObject[] Positions;
    public int _poscount = 0;
    public int platSpeed = 1;
    public bool standOn = false;

    private float startTime;
    private float journeyLength;


    // Start is called before the first frame update
    void Start()
    {

        if (standOn)
        {
            platSpeed = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (!lerp)
        {
            float step = platSpeed * Time.deltaTime;
            if (Vector2.Distance(gameObject.transform.position, Positions[_poscount].transform.position) >= 0.5f)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Positions[_poscount].transform.position, step);
            }
            else if (_poscount < Positions.Length - 1)
            {
                _poscount++;
            }
            else
            {
                _poscount = 0;
            }

        }
        else if (lerp)
        {

            float step = platSpeed * Time.deltaTime;

            if (Vector2.Distance(gameObject.transform.position, Positions[_poscount].transform.position) >= 0.2f)
            {
                float disCovered = (Time.time - startTime) * platSpeed;
                float fractionofJourney = disCovered / journeyLength;
                gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, Positions[_poscount].transform.position, fractionofJourney * Time.deltaTime);
            }
            else if (_poscount < Positions.Length - 1)
            {
                ++_poscount;
                startTime = Time.time;
                journeyLength = Vector2.Distance(Positions[_poscount - 1].transform.position, Positions[_poscount].transform.position);


            }
            else
            {
                _poscount = 0;
                startTime = Time.time;
                journeyLength = Vector2.Distance(Positions[Positions.Length - 1].transform.position, Positions[_poscount].transform.position);

            }

        }


    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.gameObject.transform;
            if (standOn)
            {
                platSpeed = 1;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            if (standOn)
            {
                platSpeed = 0;
            }
        }
    }
}
