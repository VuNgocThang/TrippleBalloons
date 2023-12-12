using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DoSomething : MonoBehaviour
{
    [SerializeField] List<Transform> From;
    [SerializeField] List<Transform> To;
    public void SetPosition()
    {
        for (int i = 0; i < To.Count; ++i)
            To[i].gameObject.SetActive(false);
        for (int i = 0; i < From.Count; ++i)
        {
            To[i].gameObject.SetActive(true);
            To[i].position = From[i].position;

            To[i].localScale = From[i].localScale;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(DoSomething)), CanEditMultipleObjects]
public class DoSomething_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DoSomething myScript = (DoSomething)target;
        if (GUILayout.Button("Set Position"))
        {
            myScript.SetPosition();
        }
    }
}
#endif