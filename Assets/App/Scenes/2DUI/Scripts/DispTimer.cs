using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DispTimer : MonoBehaviour
{
    public TextMeshProUGUI text;

    GameObject objGameManager;

    // Start is called before the first frame update
    void Start()
    {
        objGameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        bool isReverse = objGameManager.GetComponent<GameStateContoller>().IsReverse();
        float counter;
        if(isReverse)
        {
            counter = objGameManager.GetComponent<GameStateContoller>().GetGameReverseCounter();
            int counterInt = (int)counter;
            text.text = "RTimer:" + counterInt.ToString();
        }
        else
        {
            counter = objGameManager.GetComponent<GameStateContoller>().GetGameCounter();
            int counterInt = (int)counter;
            text.text = "Timer:" + counterInt.ToString();
        }
    }
}
