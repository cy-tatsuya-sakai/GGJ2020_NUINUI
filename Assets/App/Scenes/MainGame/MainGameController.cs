﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject testObj;
    [SerializeField] private MarkingPinManager _markingPinManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        testObj.transform.position = Util.GetMouseWorldPosition(_cam);

        if(Input.GetMouseButtonDown(0))
        {
            var pin = _markingPinManager.GetMarkingPin(testObj.transform.position);
            var pos = (pin == null) ? testObj.transform.position : pin.transform.position;
            _markingPinManager.AddPin(pos);
        }
    }
}
