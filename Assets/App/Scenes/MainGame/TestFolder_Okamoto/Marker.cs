using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public Camera cam;
    Vector3 screenPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        transform.position = Util.GetMouseWorldPosition(cam);
    }
}
