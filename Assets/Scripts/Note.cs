using UnityEngine;

public class Note : MonoBehaviour
{
    float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("hello");
        speed = 1; 
    }


    // Update is called once per frame
    void Update()
    {
        //transform.position -= transform.forward * Time.deltaTime;
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}