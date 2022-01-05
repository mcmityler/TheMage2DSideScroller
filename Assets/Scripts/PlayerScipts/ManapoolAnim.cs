using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManapoolAnim : MonoBehaviour
{
    public float animTime = 10f;
    public float _c = 0;
    public float _t = 0;
    public float timeAnimation = 0.2f;
    public Sprite[] manaAnimation;
    private int currentSprite = 0;
    // Update is called once per frame
    void Update()
    {
        if (_c <= animTime)
        {
            _c += Time.deltaTime;
        }
        if (_c >= animTime)
        {
            _t += Time.deltaTime;
            if (_t >= timeAnimation)
            {
                if (currentSprite >= manaAnimation.Length - 1)
                {
                    currentSprite = 0;
                    _c = 0;
                }
                else
                {
                    currentSprite++;
                }

                gameObject.GetComponent<Image>().sprite = manaAnimation[currentSprite];

                _t = 0;
            }

        }

    }
}
