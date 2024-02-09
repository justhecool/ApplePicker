using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwigInstantiator : MonoBehaviour
{
   [SerializeField] private float instantiateInterval = 2.5f;
    [SerializeField] private GameObject twigPrefab = null;
    public static float bottomY = -10f;

    private List<GameObject> twigInstances = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(InstantiateTwig), instantiateInterval, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(bottomY, -10f, 10f);
        for (int i = twigInstances.Count - 1; i >= 0; i--)
        {
            if (twigInstances[i].transform.position.y < bottomY)
            {
                Destroy(twigInstances[i]);
                twigInstances.RemoveAt(i);
            }
        }
    }
    public void RemoveTwig(GameObject twig)
    {
        twigInstances.Remove(twig);
    }

    private void InstantiateTwig()
    {
        GameObject twigInstance = Instantiate(twigPrefab, transform.position, transform.rotation);
        twigInstances.Add(twigInstance);
    }
}
