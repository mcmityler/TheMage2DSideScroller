using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    [SerializeField] private float cloudSpeed = 0.2f;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private GameObject startPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x - (cloudSpeed * Time.deltaTime), gameObject.transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("endpoint"))
        {
            gameObject.transform.position = new Vector3(startPoint.transform.position.x, gameObject.transform.position.y);
        }
    }
}
