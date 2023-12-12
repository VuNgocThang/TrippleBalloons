using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CameraResize : MonoBehaviour
{
    //public CinemachineFreeLook freeLock;
    public Camera Cam;
    public float baseSize;
    public float baseScreenRatio;
    public float rateY;
    public GameObject a;
    public CanvasScaler scaler;


    public void InitSizeObject(GameObject obj)
    {
        float currentRatio = Cam.aspect;
        if (currentRatio <= baseScreenRatio)
        {
            //freeLock.m_Lens.FieldOfView = baseSize * baseScreenRatio / currentRatio;
            Cam.fieldOfView = baseSize * baseScreenRatio / currentRatio;
            float xyz = rateY * (baseScreenRatio / currentRatio - 1);
            a.transform.localPosition += new Vector3(0, xyz);
            scaler.matchWidthOrHeight = 0;
            Cam.transform.position += new Vector3(0, 0, xyz * 8);
        }
        else
        {
            //freeLock.m_Lens.FieldOfView = baseSize / baseScreenRatio * currentRatio;
            //a.transform.localPosition += new Vector3(0, rateY * (currentRatio / baseScreenRatio - 1));
            scaler.matchWidthOrHeight = 1;
        }
    }
}

