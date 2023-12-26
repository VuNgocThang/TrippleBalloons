using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject none;

    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            Instantiate(a, transform);
        }
        for (int i = 0; i < 2; i++)
        {
            Instantiate(b, transform);
        }
    }
}
