using System.Collections;
using System.Collections.Generic;
using Lib.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        SceneManager.LoadScene("MainGame");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
