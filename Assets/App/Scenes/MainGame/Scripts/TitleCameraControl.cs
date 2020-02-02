using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraControl : MonoBehaviour
{
    float counter;
    [SerializeField, Header("1周するスピード")]
    float turnSpeed = 0.1f;
    [SerializeField, Header("傾く角度")]
    float turnAngle = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Sin(counter) * turnAngle;
        transform.rotation = Quaternion.AngleAxis(y, new Vector3(0, 1, 0));


        // counter
        counter += Time.deltaTime * turnSpeed;
    }
}
