using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public PathCreator creator;
    public float speed = 5f;
    float distance;

    private void Start()
    {
        creator = LogicGame.instance.pathCreater;
    }
    private void Update()
    {
        distance += speed * Time.deltaTime;
        transform.position = creator.path.GetPointAtDistance(distance);
        //transform.rotation = creator.path.GetRotationAtDistance(distance);
    }
}
