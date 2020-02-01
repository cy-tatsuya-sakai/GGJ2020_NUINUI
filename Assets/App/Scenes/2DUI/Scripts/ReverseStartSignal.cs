using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseStartSignal : MonoBehaviour
{
    // 編集可能
    [SerializeField, Header("Readyが表示されるまでの時間")]
    float CounterForDisplayReady = 1.0f;
    [SerializeField, Header("ReadyからGoが表示されるまでの時間")]
    float CounterForDisplayGo = 2.0f;
    [SerializeField, Header("Goからゲームが始まるまでの時間")]
    float CounterForGameStart = 1.0f;

    // private
    GameObject ObjReady;
    GameObject ObjStart;
    float SignalTimer;

    // Game Manager
    GameObject objGameManager;

    // Start is called before the first frame update
    void Start()
    {
        ObjReady = GameObject.Find("ReReady");
        ObjStart = GameObject.Find("ReGo");

        ObjReady.SetActive(false);
        ObjStart.SetActive(false);

        SignalTimer = 0;

        // Game Manager
        objGameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        float timeDispReady = CounterForDisplayReady;
        float timeDispGo = CounterForDisplayReady + CounterForDisplayGo;
        float timeStart = CounterForDisplayReady + CounterForDisplayGo + CounterForGameStart;

        if (SignalTimer > timeStart)
        {
            ObjReady.SetActive(false);
            ObjStart.SetActive(false);
        }
        else if (SignalTimer > timeDispGo)
        {
            ObjReady.SetActive(false);
            ObjStart.SetActive(true);
        }
        else if (SignalTimer > timeDispReady)
        {
            ObjReady.SetActive(true);
            ObjStart.SetActive(false);
        }
        else
        {
            ObjReady.SetActive(false);
            ObjStart.SetActive(false);
        }

        // Count Timer
        // Game Manager
        bool isReverse = objGameManager.GetComponent<GameStateContoller>().IsReverse();
        if(isReverse)
        {
            SignalTimer += Time.deltaTime;
        }
    }
}
