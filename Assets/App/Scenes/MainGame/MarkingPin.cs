using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// なんかマチ針
/// </summary>
public class MarkingPin : MonoBehaviour
{
    public int Index { get; set; }  // マチ針リストの何番目か。単に作られた順番を示す

    public void Init(int idx)
    {
        Index = idx;
    }
}
