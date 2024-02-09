using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class BasketInstantiator : MonoBehaviour
{
    [SerializeField] private float instantiateInterval = 2.5f;
    [SerializeField] public GameObject basketPrefab = null;
    public static float bottomY = -10f;

    private List<GameObject> basketInstances = new List<GameObject>();

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        InvokeRepeating(nameof(InstantiateBasket), instantiateInterval, 3);
        basketPrefab.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(bottomY, -10f, 10f);
        for (int i = basketInstances.Count - 1; i >= 0; i--)
        {
            if (basketInstances[i].transform.position.y < bottomY)
            {
                Destroy(basketInstances[i]);
                basketInstances.RemoveAt(i);
            }
        }
    }
    public void RemoveBasket(GameObject basket)
    {
        basketInstances.Remove(basket);
    }

    // public void call()
    // {
    //     // InvokeRepeating(nameof(InstantiateBasket), 2.5f, 3f);
    //     //call instantiateBasket every 3 seconds
    //     // InvokeRepeating(nameof(InstantiateBasket), 1, 3f);
    //     InstantiateBasket();
    // }

    private void InstantiateBasket()
    {
        GameObject basketInstance = Instantiate(basketPrefab, transform.position, transform.rotation);
        basketInstances.Add(basketInstance);
    }
}
