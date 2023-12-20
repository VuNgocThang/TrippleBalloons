using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public EasyController easyController;
    public Transform nX;

    [SerializeField] TutorialManager tutorialManager;
    private void Update()
    {
        Vector3 dv = easyController.dV;
        if (GameManager.Instance.canRotate)
        {
            if (dv != Vector3.zero)
            {
                tutorialManager.AnimHandRotate();
                nX.localEulerAngles += new Vector3(0, -dv.x, 0) * 10f * Time.deltaTime;
                float f = Mathf.Clamp(transform.localEulerAngles.x - dv.y * 5f * Time.deltaTime, 0, 50);
                transform.localEulerAngles = new Vector3(f, 0, 0);
                LogicGame.instance.UpdateLine();
            }
        }
    }
}
