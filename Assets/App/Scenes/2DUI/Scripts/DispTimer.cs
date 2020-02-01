using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DispTimer : MonoBehaviour
{
    public Text text;

    GameObject objGameManager;

    // Start is called before the first frame update
    void Start()
    {
        objGameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        float counter = objGameManager.GetComponent<GameStateContoller>().GetGameCounter();
        text.text = "Text";
    }
}
