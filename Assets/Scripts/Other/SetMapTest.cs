using System.Collections.Generic;
using UnityEngine;

public class SetMapTest : MonoBehaviour
{
    public GameObject cubePrefab;
    public List<float> radiuss = new List<float>();
    public List<float> yAxis = new List<float>();
    public List<int> numbers = new List<int>();

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < yAxis.Count; i++)
        {
            int number = numbers[i];
            for (int j = 0; j < number; j++)
            {
                float randomRange = Random.Range(0f, 0.5f);
                float angle = j * (360 / number) * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * radiuss[i];
                float z = Mathf.Sin(angle) * radiuss[i];
                Vector3 spawnPoint = new Vector3(x, yAxis[i] + randomRange, z);
                Instantiate(cubePrefab, spawnPoint, Quaternion.identity, transform);
            }
        }
    }


}
