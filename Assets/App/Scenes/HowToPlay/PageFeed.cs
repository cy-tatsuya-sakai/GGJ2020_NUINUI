using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFeed : MonoBehaviour
{
    [SerializeField] private GameObject[] howTos;
    [SerializeField] private GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        Page1();
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (howTos[0].activeInHierarchy)
            {
                Page2();
            }
            else if (howTos[1].activeInHierarchy)
            {
                Page3();
            }
            else if (howTos[2].activeInHierarchy)
            {
                Page1();
                button.SetActive(true);
            }
        }
    }

    void Page1()
    {
        howTos[0].SetActive(true);
        howTos[1].SetActive(false);
        howTos[2].SetActive(false);
    }

    void Page2()
    {
        howTos[0].SetActive(false);
        howTos[1].SetActive(true);
        howTos[2].SetActive(false);
    }

    void Page3()
    {
        howTos[0].SetActive(false);
        howTos[1].SetActive(false);
        howTos[2].SetActive(true);
    }
}
