using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    private bool isClicked = false;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        
        DetermineIfMousePress();
        DetermineIfMouseClickReleased();
        if (isClicked)
        {
            transform.position = new Vector3(DetermineBasketXPosition(), transform.position.y, transform.position.z);
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
