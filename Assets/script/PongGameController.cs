using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PongGameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    uint player1Score;
    uint player2Score;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        player1Score = 0;
        player2Score = 0;

        scoreText.text = player1Score.ToString() + " : " + player2Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementScore(bool isPlayer1)
    {
        if(isPlayer1)
        {
            player1Score++;
        }
        else 
        {
            player2Score++;
        }

        scoreText.text = player1Score.ToString() + " : " + player2Score.ToString();
    }

}
