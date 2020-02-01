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
        float counter = objGameManager.GetComponent<GameStateContoller>().GetGameCounter();
        int counterInt = (int)counter;
        text.text = "Timer:" + counterInt.ToString();
    }
}
