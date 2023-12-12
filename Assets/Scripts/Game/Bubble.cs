using System;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int ID;
    public Vector3 originalPos;
    public Vector3 originalScale;

    public bool click;
    public bool hasChildren;
    public bool isChild;
    public bool canMoveHT;
    public bool IsMoving;
    public bool IsDone;

    public float radius;
    public Transform children;
    public Transform connectPoint;
    public Material mat;
    public Texture2DArray bubblTexture2d;
    public List<GameObject> objs = new List<GameObject>();
    public List<Material> mats = new List<Material>();
    public MeshCollider meshCollider;
    public Rigidbody rb;
    public Tweener tweenerMove;
    public GameObject particleBoom;
    public GameObject particlePP;
    public ParticleSystem particleEatt;
    public bool CanMoving => !IsMoving && !IsDone;

    private void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
    }
    private void Update()
    {
        MoveHT();
        transform.localRotation = Quaternion.Euler(new Vector3(0f, transform.localRotation.y, 0f));
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].activeSelf)
            {
                objs[i].transform.localEulerAngles += new Vector3(0f, 1f * Time.deltaTime * 20f, 0f);

            }
        }
    }


    public void Init(int id)
    {
        ID = id;
        SetColor(id);
    }
    public void SetColor(int index)
    {
        int i = SetIndexObjs(index);
        objs[i].SetActive(true);
        if (hasChildren)
        {
            SelectMaterial(1);
        }
        else
        {
            SelectMaterial(0);
        }
        objs[i].GetComponent<MeshRenderer>().material.SetFloat("_Index", index + 1);
    }
    public int SetIndexObjs(int i)
    {
        int result;

        switch (i / 9)
        {
            case 0:
                result = 0;
                break;
            case 1:
                result = 1;
                break;
            case 2:
                result = 2;
                break;
            default:
                result = 3;
                break;
        }
        return result;
    }

    public void Move(Transform parent, float time = -1, Action checkEat = null)
    {
        IsMoving = true;
        transform.SetParent(parent);
        tweenerMove = transform.DOLocalMove(Vector3.zero, time == -1 ? 0.3f : time);
        transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.3f);
        StateAfterMove();
        SelectMaterial(0);
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].activeSelf)
            {
                objs[i].GetComponent<MeshRenderer>().material.SetFloat("_Index", ID + 1);
            }
        }

        tweenerMove.OnStart(() =>
        {
            LogicGame.instance.count += 1;
        });
        tweenerMove.OnComplete(() =>
        {
            LogicGame.instance.count -= 1;
            IsMoving = false;
            checkEat?.Invoke();
        });
    }
    public void MoveHT()
    {
        if (canMoveHT)
        {
            rb.velocity += (LogicGame.instance.targetHT.position - transform.position) * Time.deltaTime;
        }
    }
    public void SelectMaterial(int index)
    {
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].activeSelf)
            {
                objs[i].GetComponent<MeshRenderer>().sharedMaterial = mats[index];
            }
        }
    }
    public void CheckHasChild()
    {
        //if (children.childCount > 0)
        //{
        //    hasChildren = true;
        //}

        //if (hasChildren)
        //{
        for (int i = 0; i < children.childCount; i++)
        {
            Bubble child = children.GetChild(i).GetComponent<Bubble>();
            SetStateIfIsChild(child);
        }
        //}
    }

    public void SetStateIfIsChild(Bubble bb)
    {
        bb.isChild = true;
        bb.canMoveHT = false;
        bb.meshCollider.enabled = false;
        bb.rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void ResetState(Bubble bb)
    {
        bb.isChild = false;
        bb.canMoveHT = true;
        bb.meshCollider.enabled = true;
        bb.rb.constraints &= ~RigidbodyConstraints.FreezePosition; // bỏ freeze pos
        bb.rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
    }
    public void StateAfterMove()
    {
        meshCollider.enabled = false;
        canMoveHT = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    public void ResetStateIfUndo()
    {
        //meshCollider.enabled = true;
        canMoveHT = true;
        rb.constraints &= ~RigidbodyConstraints.FreezePosition;
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        transform.localRotation = Quaternion.Euler(new Vector3(0f, transform.localRotation.y, 0f));
    }

    public void InitBBInUI(int id)
    {
        ID = id;
        SetColor(id);
        canMoveHT = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
    }
}
