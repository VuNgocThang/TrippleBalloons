using UnityEngine;
using UnityEngine.EventSystems;

public class EasyController : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] protected RectTransform rectBG;
    [SerializeField] protected Transform transControl;

    protected Vector3 startPos;
    protected Vector3 currentPos;
    protected Vector3 dPos;
    public Vector3 V;
    Vector3 lastV;
    public Vector3 dV => GetDv();

    Vector3 GetDv()
    {
        Vector3 result = V - lastV;
        lastV = V;
        return result;
    }

    protected Vector3 defaultPos;

    void Start()
    {
        if (rectBG != null) defaultPos = rectBG.localPosition;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        startPos = eventData.position;
        currentPos = eventData.position;
        dPos = Vector3.zero;
        lastV = Vector3.zero;

        if (rectBG != null)
        {
            Vector3 p = startPos;
            p.x /= transform.lossyScale.x;
            p.y /= transform.lossyScale.y;
            rectBG.localPosition = p;
            transControl.localPosition = Vector3.zero;
        }
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        LogicGame.instance.isDrag = true;
        currentPos = eventData.position;
        V = currentPos - startPos;

        if (transControl != null)
        {
            transControl.localPosition = V.normalized * rectBG.sizeDelta.x / 2;
            transControl.eulerAngles = new Vector3(0, 0, Mathf.Atan2(transControl.localPosition.y, transControl.localPosition.x) * Mathf.Rad2Deg);
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {

    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        V = Vector3.zero;
        dPos = Vector3.zero;
        currentPos = Vector3.zero;
        lastV = Vector3.zero;

        if (rectBG != null)
        {
            rectBG.localPosition = defaultPos;
            transControl.localPosition = Vector3.zero;
        }
    }
}

