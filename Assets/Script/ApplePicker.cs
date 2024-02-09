using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplePicker : MonoBehaviour
{
    [SerializeField] private GameObject basketPrefab = null;
    [SerializeField] public int numBaskets = 4;
    [SerializeField] private float basketBottomY = -14f;
    [SerializeField] private float basketSpacingY = 2f;
    public List<GameObject> basketList;
    [SerializeField] private Image refreshButton = null;
    private int basketIndex;
    // Start is called before the first frame update
    void Start()
    {
        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
        
        var texture = UnityEditor.EditorGUIUtility.IconContent("Refresh").image;
        refreshButton.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        // color = white
        refreshButton.color = Color.white;
        // on click restart game
        refreshButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Game"));
   
    }

    // Update is called once per frame
    void Update()
    {
         basketIndex = basketList.Count;
    }

    public void BasketDestroyed()
    {
        // GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        // foreach (GameObject tGO in tAppleArray)
        // {
        //     Destroy(tGO);
        // }
        basketIndex--;
        GameObject tBasketGO = basketList[basketIndex];
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);

        if (basketList.Count == 0)
        {
            SceneManager.LoadScene("gameOver");
            // update high score
        }
    }

    internal void BasketAdded()
    {
        // does the current number baskets exceed max?
        if (basketIndex != numBaskets)
        {
            GameObject tBasketGO = Instantiate(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * basketList.Count);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
        return;
    }
}
