using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathEnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject[] Corners;
    [SerializeField] private GameObject frozenBlock;
    [SerializeField] private Sprite[] fBlockAnim;
    private int fAnimCount = 0;
    private bool frozen = false;
    private float freezeCounter = 0;
    [SerializeField] private float freezeTime = 2;

    private Vector3 start;
    private Vector3 diff;

    [SerializeField] private float timer = 0;
    [SerializeField] private float Seconds = 5;
    private int cornerNum = 0;
    private float percent = 0;
    // Start is called before the first frame update
    void Start()
    {
        cornerNum = 1;
        frozen = false;
        fAnimCount = 0;
        frozenBlock.SetActive(false);
        start = gameObject.transform.position;
        diff = Corners[cornerNum].transform.position - gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen)
        {
            if (timer <= Seconds)
            {
                timer += Time.deltaTime;
                percent = timer / Seconds;
                transform.position = start + diff * percent;

            }
            else if (timer > Seconds)
            {

                timer = 0;
                cornerNum++;
                if (cornerNum >= Corners.Length)
                {
                    cornerNum = 0;
                }
                start = gameObject.transform.position;
                diff = Corners[cornerNum].transform.position - gameObject.transform.position;
            }
        }
        if (frozen)
        {

            freezeCounter += Time.deltaTime;
            if (freezeCounter >= freezeTime)
            {
                freezeCounter = 0;
                fAnimCount++;
                if (fAnimCount >= fBlockAnim.Length)
                {
                    frozenBlock.SetActive(false);
                    frozen = false;
                    fAnimCount = 0;
                }
                frozenBlock.GetComponent<SpriteRenderer>().sprite = fBlockAnim[fAnimCount];
            }
        }


    }

    public void Freeze()
    {
        frozen = true;
        frozenBlock.SetActive(true);
        fAnimCount = 0;
        freezeCounter = 0;
    }
}
