using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public Transform startPoint;
    LineRenderer lineRenderer;
    LineRenderer LineRenderer
    {
        get
        {
            if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
            return lineRenderer;
        }
    }
    public void CreateLine(List<Bubble> endPoints)
    {
        LineRenderer.positionCount = endPoints.Count * 2;

        for (int i = 0; i < LineRenderer.positionCount; i++)
        {
            if (i % 2 == 0)
            {
                LineRenderer.SetPosition(i, startPoint.position);
            }
            else
            {
                LineRenderer.SetPosition(i, endPoints[i / 2].connectPoint.position);
            }
        }
    }

}
