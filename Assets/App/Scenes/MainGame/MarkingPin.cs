using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// なんかマチ針
/// </summary>
public class MarkingPin : MonoBehaviour
{
    public int Index { get; set; }  // マチ針リストの何番目か。単に作られた順番を示す

    void Start()
    {
        transform.DOPunchScale(Vector3.one * 1.3f, 0.3f);
    }

    public void Init(int idx)
    {
        Index = idx;
    }
}
