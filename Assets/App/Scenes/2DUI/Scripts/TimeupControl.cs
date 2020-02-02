using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TimeupControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.transform.DOScale(Vector3.one, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
