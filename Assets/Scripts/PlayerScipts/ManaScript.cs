using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaScript : MonoBehaviour
{
    public Sprite[] ManabarLeftSprites;
    public GameObject ManabarLeft;
    public Sprite[] ManabarMiddleSprites;
    public GameObject ManabarMiddle;
    public Sprite[] ManabarRightSprites;
    public GameObject ManabarRight;

    public GameObject[] manapoolObj;

    private const int MAXMANA = 7;
    public int manapool = 4;

    public int currentMana = 0;

    public float manaRegenTime = 3;
    private float _manaTimer = 0;
    private float _manaregeneffectTimer = 0;
    private float _manaregeneffectTime = 0.5f;




    // Start is called before the first frame update
    void Start()
    {

    }
    public void loadMana(int _savedMana)
    {
        manapool = _savedMana;
        currentMana = manapool;
        for (int i = 0; i < MAXMANA; i++)
        {
            manapoolObj[i].GetComponent<Image>().enabled = false;
        }
        for (int i = 0; i < manapool; i++)
        {
            manapoolObj[i].GetComponent<Image>().enabled = true;
        }
        Manabar();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentMana < manapool)
        {
            _manaTimer += Time.deltaTime;

            if (_manaTimer >= manaRegenTime)
            {
                _manaregeneffectTimer += Time.deltaTime;
                if (_manaregeneffectTimer >= _manaregeneffectTime)
                {
                    RegenerateManaOverTime();
                    _manaregeneffectTimer = 0;
                }


            }
        }
        else if (currentMana == manapool)
        {
            _manaTimer = 0;
        }
    }

    private void RegenerateManaOverTime()
    {
        if (currentMana < manapool)
        {
            currentMana++;
            ManabarObjects();
        }
    }
    private void ManabarObjects()
    {
        for (int i = 0; i < manapool; i++)
        {
            manapoolObj[i].GetComponent<Image>().enabled = false;
        }
        for (int i = 0; i < currentMana; i++)
        {
            manapoolObj[i].GetComponent<Image>().enabled = true;
        }
    }

    public bool CastMana(int _manacost)
    {
        if (currentMana - _manacost >= 0)
        {
            currentMana -= _manacost;
            _manaTimer = 0;
            ManabarObjects();
            return true;

        }
        else
        {
            return false;
        }
    }

    public void ManaIncrease()
    {
        if (manapool < MAXMANA)
        {
            manapool++;
            currentMana = manapool;
        }
        Manabar();
        ManabarObjects();
    }

    public void ManaPickup(int _manaregenamount)
    {
        if (currentMana + _manaregenamount <= manapool)
        {
            currentMana += _manaregenamount;
        }
        else
        {
            currentMana = manapool;
        }
        ManabarObjects();

    }
    public void Manabar()
    {
        switch (manapool)
        {
            case 4:
                ManabarLeft.GetComponent<Image>().sprite = ManabarLeftSprites[0];
                ManabarMiddle.GetComponent<Image>().sprite = ManabarMiddleSprites[0];
                ManabarRight.GetComponent<Image>().enabled = false;
                break;
            case 5:
                ManabarLeft.GetComponent<Image>().sprite = ManabarLeftSprites[1];
                ManabarMiddle.GetComponent<Image>().sprite = ManabarMiddleSprites[1];
                ManabarRight.GetComponent<Image>().enabled = true;
                ManabarRight.GetComponent<Image>().sprite = ManabarRightSprites[0];
                break;
            case 6:
                ManabarLeft.GetComponent<Image>().sprite = ManabarLeftSprites[2];
                ManabarMiddle.GetComponent<Image>().sprite = ManabarMiddleSprites[2];
                ManabarRight.GetComponent<Image>().enabled = true;
                ManabarRight.GetComponent<Image>().sprite = ManabarRightSprites[1];
                break;
            case 7:
                ManabarLeft.GetComponent<Image>().sprite = ManabarLeftSprites[3];
                ManabarMiddle.GetComponent<Image>().sprite = ManabarMiddleSprites[3];
                ManabarRight.GetComponent<Image>().enabled = true;
                ManabarRight.GetComponent<Image>().sprite = ManabarRightSprites[2];
                break;

        }
    }
}
