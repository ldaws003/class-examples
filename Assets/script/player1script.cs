using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1script : MonoBehaviour
{
    public float Spd;
    Rigidbody2D rb;
    public bool player1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(player1)
        {
            if(Input.GetKey(KeyCode.W))
            {
                //rb.AddForce(Vector2.up * Spd * Time.deltaTime);
                transform.Translate(0.0f, Spd * Time.deltaTime, 0);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                //rb.AddForce(Vector2.down * Spd * Time.deltaTime);
                transform.Translate(0.0f, -Spd * Time.deltaTime, 0);
            }
        }
        else
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0.0f, Spd * Time.deltaTime, 0);
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0.0f, -Spd * Time.deltaTime, 0);
            }
        }
    }
}
