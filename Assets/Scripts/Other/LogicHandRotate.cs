using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicHandRotate : MonoBehaviour
{
    public PathCreator creator;
    public float speed = 0.2f;
    float distance;

    private void Update()
    {
        if (transform != null)
        {
            distance += speed * Time.deltaTime;
            transform.position = Camera.main.WorldToScreenPoint(creator.path.GetPointAtDistance(distance));
        }
    }
}
