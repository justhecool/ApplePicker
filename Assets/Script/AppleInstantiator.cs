using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleInstantiator : MonoBehaviour
{
    [SerializeField] private float instantiateInterval = 1.0f;
    [SerializeField] private GameObject applePrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(InstantiateApple), instantiateInterval, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.PingPong(Time.time, 1);
    }

    private void InstantiateApple()
    {
        Instantiate(applePrefab, transform.position, transform.rotation);
    }
}
