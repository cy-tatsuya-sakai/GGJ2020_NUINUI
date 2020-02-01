using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Camera cam;
    public Transform Marker;
    private float distance_two;
    public float speed = 1.0F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Marker);
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = gameObject.transform.position;
            distance_two = Vector3.Distance(pos, Marker.position);
            float present_Location = (Time.time * speed) / distance_two;
            transform.position = Vector3.Lerp(pos, Marker.position, present_Location);
        }
    }
}
