using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Basket : MonoBehaviour
{
    private bool isClicked = false;
    private GameObject scoreGT = null;
    void Start()
    {
        // convert scoreGT to Text
        scoreGT = GameObject.FindWithTag("scoreCount");
        // Text scoreText = scoreGT.GetComponentInChildren<Text>();
        scoreGT.GetComponent<TMP_Text>().text = "0";         

    }
    void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.tag == "Apple")
    {
        // Notify AppleInstantiator that the apple has been caught
        AppleInstantiator appleInstantiator = FindObjectOfType<AppleInstantiator>();
        appleInstantiator.RemoveApple(collision.gameObject);

        // Destroy the caught apple
        Destroy(collision.gameObject);

        // Update the score
        int score = int.Parse(scoreGT.GetComponent<TMP_Text>().text);
        score += 10;
        scoreGT.GetComponent<TMP_Text>().text = score.ToString();
        PlayerPrefs.SetInt("Score", score);
        // [Initial Case] if highScore is 0, set it to score
        if (PlayerPrefs.GetInt("HighScore") == 0)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        // else then update high score
        else if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    if (collision.gameObject.tag == "Twig")
    {
        // Notify TwigInstantiator that the twig has been caught
        TwigInstantiator twigInstantiator = FindObjectOfType<TwigInstantiator>();
        twigInstantiator.RemoveTwig(collision.gameObject);
        // Destroy the caught twig
        Destroy(collision.gameObject);

        // remove 1 basket
        ApplePicker applePicker = FindObjectOfType<ApplePicker>();
        applePicker.BasketDestroyed();

    }

    if (collision.gameObject.tag == "BasketFly")
    {
        // Notify BasketInstantiator that the basket has been caught
        BasketInstantiator basketInstantiator = FindObjectOfType<BasketInstantiator>();
        basketInstantiator.RemoveBasket(collision.gameObject);
        // Destroy the caught basket
        Destroy(collision.gameObject);
        // add 1 basket
        ApplePicker applePicker = FindObjectOfType<ApplePicker>();
        applePicker.BasketAdded();
    }

    if (collision.gameObject.tag == "goldenApple")
    {
        // Notify GoldenAppleInstantiator that the golden apple has been caught
        GoldenAppleInstantiator goldenAppleInstantiator = FindObjectOfType<GoldenAppleInstantiator>();
        goldenAppleInstantiator.RemoveApple(collision.gameObject);
        // Destroy the caught golden apple
        Destroy(collision.gameObject);

        // Update the score
        int score = int.Parse(scoreGT.GetComponent<TMP_Text>().text);
        score += 50;
        scoreGT.GetComponent<TMP_Text>().text = score.ToString();
        PlayerPrefs.SetInt("Score", score);
        // if for some reason the golden apple is caught first, set high score to score
        // [Initial Case] if highScore is 0, set it to score
        if (PlayerPrefs.GetInt("HighScore") == 0)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        // else then update high score
        else if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    
    }
}

    [System.Obsolete]
    void Update()
    {
        
        DetermineIfMousePress();
        DetermineIfMouseClickReleased();
        if (isClicked)
        {
            transform.position = new Vector3(DetermineBasketXPosition(), transform.position.y, transform.position.z);
        }

        // My custom feature:
        // determine if were in the second to last round and if the current number is less than the max number of baskets
        // get current Round
        roundHandler roundHandler = FindObjectOfType<roundHandler>();
        int currentRound = roundHandler.currentRound;
        int maxRounds = roundHandler.rounds;
        int numBaskets = FindObjectOfType<ApplePicker>().basketList.Count;
        int maxBaskets = FindObjectOfType<ApplePicker>().numBaskets;

        if ((currentRound == (maxRounds - 1) && numBaskets < maxBaskets || currentRound == (maxRounds - 2)) && numBaskets < maxBaskets)
        {
            // Throw baskets at the player to help them out & stop throwing baskets if the player has 4
            BasketInstantiator basketInstantiator = FindObjectOfType<BasketInstantiator>();
            basketInstantiator.basketPrefab.active = true;
        }
        // do we have max baskets? set the basket prefab to inactive
        else if (numBaskets == maxBaskets)
        {
            BasketInstantiator basketInstantiator = FindObjectOfType<BasketInstantiator>();
            basketInstantiator.basketPrefab.active = false;
        }
    }
    
    private float DetermineBasketXPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z -= Camera.main.transform.position.z - transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition).x;
    }

    private void DetermineIfMousePress()
    {
       if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.transform.CompareTag("Basket"))
                {                           
                    isClicked = true;         
                }
                // transform.position = hit.point;
            }
        }
    }
    private void DetermineIfMouseClickReleased()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }
    }
}
