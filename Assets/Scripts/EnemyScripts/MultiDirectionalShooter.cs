using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDirectionalShooter : MonoBehaviour
{
    public GameObject exit1;
    public GameObject exit2;
    public GameObject exit3;
    public GameObject exit4;
    public GameObject bullet;
    float timer = 0;
    float t = 0;
    float time = 3;
    bool _withinRadius = false;

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
            timer += Time.deltaTime;
            t += Time.deltaTime;
            if (timer >= 3)
            {
                timer = 0;
                Instantiate(bullet, exit1.GetComponent<Transform>().position, exit1.GetComponent<Transform>().rotation);
                Instantiate(bullet, exit2.GetComponent<Transform>().position, exit2.GetComponent<Transform>().rotation);

                Instantiate(bullet, exit3.GetComponent<Transform>().position, exit3.GetComponent<Transform>().rotation);
                Instantiate(bullet, exit4.GetComponent<Transform>().position, exit4.GetComponent<Transform>().rotation);
            }
            if (t >= 6)
            {
                t = 0;
                gameObject.transform.Rotate(new Vector3(0, 0, 1), 45);

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
            timer = 0;
            t = 0;
            _withinRadius = false;
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
