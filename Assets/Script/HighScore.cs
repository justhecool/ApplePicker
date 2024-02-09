using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour
{
    static public int score = 0;
    private GameObject highScoreGT = null;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("HighScore");
        highScoreGT = GameObject.FindWithTag("highScore");
        highScoreGT.GetComponent<TMP_Text>().text = "High Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        // highScoreGT = GameObject.FindWithTag("highScore");
        // highScoreGT.GetComponent<TMP_Text>().text = "High Score: " + score;
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore");
        }
        PlayerPrefs.SetInt("HighScore", score);
    }
}
