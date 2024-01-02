using UnityEngine;
using System.Collections.Generic;

public class DrawPoints : MonoBehaviour
{
    public GameObject linePrefab;
    private List<Vector3> pointsList = new List<Vector3>();
    private LineRenderer currentLineRenderer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateNewLine();
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("updateline?");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            if (Vector3.Distance(mousePos, pointsList[pointsList.Count - 1]) > 0.1f)
            {
                Debug.Log("updateline!");

                UpdateLine(mousePos);
            }
        }
    }

    void CreateNewLine()
    {
        GameObject lineGO = Instantiate(linePrefab);
        currentLineRenderer = lineGO.GetComponent<LineRenderer>();
        pointsList.Clear();
        pointsList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        currentLineRenderer.positionCount = 1;
        currentLineRenderer.SetPosition(0, pointsList[0]);
    }

    void UpdateLine(Vector3 newPoint)
    {
        pointsList.Add(newPoint);
        currentLineRenderer.positionCount = pointsList.Count;
        currentLineRenderer.SetPosition(pointsList.Count - 1, newPoint);
    }


}
