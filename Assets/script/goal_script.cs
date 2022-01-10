using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal_script : MonoBehaviour
{
    public bool isGoal1;
    PongGameController obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        obj = GameObject.FindWithTag("GameController").GetComponent<PongGameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if(isGoal1)
        {
            // increment the score for player one
            obj.incrementScore(true);
        }   
        else 
        {
            // incremement the score for player 2
            obj.incrementScore(false);
        }
    }
}
