using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoorScript : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject button;
    [SerializeField] private Sprite buttonDown;
    [SerializeField] private Sprite buttonUp;
    [SerializeField] private Sprite doorUp;
    [SerializeField] private Sprite doorDown;
    bool doorOpen = false;
    float _timer = 0;
    public float openTime = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen)
        {
            _timer += Time.deltaTime;
            door.GetComponent<SpriteRenderer>().sprite = doorUp;
            door.GetComponent<BoxCollider2D>().enabled = false;
            button.GetComponent<SpriteRenderer>().sprite = buttonDown;

            if (_timer >= openTime)
            {
                doorOpen = false;

            }
        }
        if (!doorOpen)
        {
            _timer = 0;
            door.GetComponent<SpriteRenderer>().sprite = doorDown;
            door.GetComponent<BoxCollider2D>().enabled = true;
            button.GetComponent<SpriteRenderer>().sprite = buttonUp;
            button.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void OpenDoor()
    {
        doorOpen = true;
        button.GetComponent<BoxCollider2D>().enabled = false;
    }
}
