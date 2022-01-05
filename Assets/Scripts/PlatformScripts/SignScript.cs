using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignScript : MonoBehaviour
{
    [SerializeField] private string stringText;
    [SerializeField] GameObject textBox;
    [SerializeField] GameObject TextPanel;
    // Start is called before the first frame update
    void Start()
    {
        TextPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            textBox.GetComponent<Text>().text = stringText;
            TextPanel.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            TextPanel.SetActive(false);
        }
    }
}
