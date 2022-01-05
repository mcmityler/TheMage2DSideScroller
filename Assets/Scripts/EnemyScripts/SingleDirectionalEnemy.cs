using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDirectionalEnemy : MonoBehaviour
{

    bool _withinRadius = false;
    bool _shot = false;
    [SerializeField] private Sprite[] enemyAnim;
    [SerializeField] private int _animNum;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bSpawn;
    float counter = 0;
    float spriteTime = 0.3f;

    [SerializeField] private GameObject frozenBlock;
    [SerializeField] private Sprite[] fBlockAnim;
    private int fAnimCount = 0;
    private bool frozen = false;
    private float freezeCounter = 0;
    [SerializeField] private float freezeTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        _withinRadius = false;
        frozenBlock.SetActive(false);
        fAnimCount = 0;
        freezeCounter = 0;
        frozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_withinRadius && !frozen)
        {
            counter += Time.deltaTime;
            if (counter >= spriteTime)
            {
                counter = 0;
                _animNum++;

            }
            if (_animNum == enemyAnim.Length - 1 && !_shot)
            {
                _shot = true;
                Shoot();
            }
            if (_animNum >= enemyAnim.Length)
            {
                _shot = false;
                _animNum = 0;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = enemyAnim[_animNum];
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
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_withinRadius && col.CompareTag("Player"))
        {
            _withinRadius = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (_withinRadius && col.CompareTag("Player"))
        {
            counter = 0;
            _animNum = 0;
            _withinRadius = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = enemyAnim[_animNum];
        }
    }
    void Shoot()
    {
        Instantiate(bullet, bSpawn.GetComponent<Transform>().position, bSpawn.GetComponent<Transform>().rotation);
    }
    public void Freeze()
    {
        frozen = true;
        frozenBlock.SetActive(true);
        fAnimCount = 0;
        freezeCounter = 0;
    }

}
