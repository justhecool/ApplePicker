using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class roundHandler : MonoBehaviour
{
     // How long will a round last?
    [SerializeField] private float roundDuration = 30f;
    [SerializeField] public int rounds = 4;
    [SerializeField] private GameObject roundTimerTxt;
    [SerializeField] private GameObject roundTxt;
    public int currentRound = 1;
    // Start is called before the first frame update
    void Start()
    {
        roundTimerTxt = GameObject.FindWithTag("roundTimer");
        roundTxt = GameObject.FindWithTag("roundCount");
        roundTimerTxt.GetComponent<TMP_Text>().text = roundDuration.ToString();
        InvokeRepeating(nameof(DecrementRoundTime), 1, 1);
        //set round
        roundTxt.GetComponent<TMP_Text>().text = "Round " + currentRound;
    }

    private void DecrementRoundTime()
    {
        roundDuration--;
        roundTimerTxt.GetComponent<TMP_Text>().text = roundDuration.ToString();
        // is round over
        if (roundDuration <= 0)
        {
            // End the current round
            EndRound();
        }
      
    }

    private void EndRound()
{
    currentRound++;
    // Check if there are more rounds
    if (currentRound <= rounds)
    {
        // Start the next round
        roundDuration = 30f;
        roundTimerTxt.GetComponent<TMP_Text>().text = roundDuration.ToString();
        roundTxt.GetComponent<TMP_Text>().text = "Round " + currentRound;
    }
    else
    {
        // End the game
        SceneManager.LoadScene("gameOver");
    }
}

    // Update is called once per frame
    void Update()
    {
        

    }
}
