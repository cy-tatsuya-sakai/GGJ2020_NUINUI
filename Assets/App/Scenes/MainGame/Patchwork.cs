using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マチ針を結んでできた三角形
/// </summary>
public class Patchwork : MonoBehaviour
{
    [SerializeField] private GameObject line1;
    [SerializeField] private GameObject line2;
    [SerializeField] private GameObject line3;

    public void Init(Vector3 a, Vector3 b, Vector3 c)
    {
        line1.transform.position = a;
        line2.transform.position = b;
        line3.transform.position = c;

        var ba = b - a;
        var cb = c - b;
        var ac = a - c;

        line1.transform.rotation = Quaternion.LookRotation(ba, Vector3.up);
        line2.transform.rotation = Quaternion.LookRotation(cb, Vector3.up);
        line3.transform.rotation = Quaternion.LookRotation(ac, Vector3.up);
        line1.transform.localScale = new Vector3(0.1f, 0.1f, ba.magnitude);
        line2.transform.localScale = new Vector3(0.1f, 0.1f, cb.magnitude);
        line3.transform.localScale = new Vector3(0.1f, 0.1f, ac.magnitude);
    }
}
