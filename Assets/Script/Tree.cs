using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distance = 5f;
    private Vector3 startPosition = new Vector3(1, 1, 1);
    // Start is called before the first frame update
    void Start()
    {   
        startPosition = (startPosition.x * Vector3.right) + transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = DetermineTreePosition();
    }

    private Vector3 DetermineTreePosition()
    {
        // the range will be .5f to 5f
        float xPosition =(Mathf.PingPong(Time.timeSinceLevelLoad * speed, 1) - .5f) * (distance );
        return new Vector3(xPosition, transform.position.y, transform.position.z);

    }
}
