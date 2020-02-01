using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public Camera cam;
    public float posz = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 markPos = Util.GetMouseWorldPosition(cam);
        markPos.z = -1;
        transform.position = markPos;
    }
}
