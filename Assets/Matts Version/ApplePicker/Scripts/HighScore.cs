using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    static public int score = 1000; //static value means this will not reset with rest of the game.

    private void Awake()
    {
        //Search playerPrefs key list to see if there is an existing high score
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }
        //Assign the high score
        PlayerPrefs.SetInt("HighScore", score);
    }


    void Update()
    {
        //grab high score text object and display it
        Text gt = this.GetComponent<Text>();
        gt.text = "High Score" + score;
        //update the highscore if necessary
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
       
    }
}
