using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressAnyButtonControl : MonoBehaviour
{
    float counter;
    float alfa;
    [SerializeField, Header("1周するスピード")]
    float speed = 10.0f;
    float red, green, blue;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0.0f;
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        alfa = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        alfa =1.0f + Mathf.Cos(counter);
        if(alfa>1.0f)
        {
            alfa = 1.0f;
        }

        GetComponent<Image>().color = new Color(red, green, blue, alfa);

        // counter
        counter += Time.deltaTime * speed;
    }
}
