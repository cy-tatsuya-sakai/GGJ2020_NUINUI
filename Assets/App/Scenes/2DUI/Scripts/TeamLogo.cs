using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamLogo : MonoBehaviour
{
    GameObject objLogo;
    float counter;
    [SerializeField, Header("1周するスピード")]
    float turnSpeed = 2.0f;
    [SerializeField, Header("傾く角度")]
    float turnAngle = 15.0f;


    // Start is called before the first frame update
    void Start()
    {
        objLogo = GameObject.Find("TeamLogo");
        counter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float z = Mathf.Sin(counter) * turnAngle;
        objLogo.transform.rotation = Quaternion.AngleAxis(90.0f + z, new Vector3(0, 0, 1));


        // counter
        counter += Time.deltaTime * turnSpeed;
    }
}
