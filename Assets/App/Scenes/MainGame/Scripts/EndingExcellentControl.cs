using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingExcellentControl : MonoBehaviour
{
    [SerializeField, Header("キー入力待ち時間")]
    float WaitForInput = 2.0f;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > WaitForInput)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("Title");
            }
        }

        // counter
        counter += Time.deltaTime;
    }
}
