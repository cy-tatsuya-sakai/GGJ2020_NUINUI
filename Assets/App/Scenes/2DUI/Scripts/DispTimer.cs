using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DispTimer : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Game Manager
    GameObject objGameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Game Manager
        objGameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        // Game Manager
        bool isReverse = objGameManager.GetComponent<GameStateContoller>().IsReverse();

        float counter;
        if(isReverse)
        {
            counter = objGameManager.GetComponent<GameStateContoller>().GetGameReverseCounter();
            int counterInt = (int)counter;
            text.text = "ReTimer:" + counterInt.ToString();
        }
        else
        {
            counter = objGameManager.GetComponent<GameStateContoller>().GetGameCounter();
            int counterInt = (int)counter;
            text.text = "Timer:" + counterInt.ToString();
        }
    }
}
