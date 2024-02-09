using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleInstantiator : MonoBehaviour
{
    [SerializeField] private float instantiateInterval = 1.0f;
    [SerializeField] private GameObject applePrefab = null;
    
    public static float bottomY = -10f;
    private List<GameObject> appleInstances = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(InstantiateApple), instantiateInterval, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(bottomY, -10f, 10f);
        for (int i = appleInstances.Count - 1; i >= 0; i--)
        {
            if (appleInstances[i].transform.position.y < bottomY)
            {
                Destroy(appleInstances[i]);
                appleInstances.RemoveAt(i);
            }
        }
    }
    public void RemoveApple(GameObject apple)
    {
        appleInstances.Remove(apple);
    }

    private void InstantiateApple()
    {
        GameObject appleInstance = Instantiate(applePrefab, transform.position, transform.rotation);
        appleInstances.Add(appleInstance);
    }
}
