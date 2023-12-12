using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public EasyController easyController;
    public CinemachineFreeLook freeLock;
    public bool isRotate;
    public float yourMultiplierY = 0.005f;
    public float yourMultiplierX = 0.02f;
    private void Update()
    {
        Vector3 dv = easyController.dV;
        if (GameManager.Instance.canRotate)
        {
            if (dv != Vector3.zero)
            {
                isRotate = true;
                freeLock.m_XAxis.Value += dv.x * yourMultiplierX;
                freeLock.m_YAxis.Value -= dv.y * yourMultiplierY;
            }
        }
    }
}
