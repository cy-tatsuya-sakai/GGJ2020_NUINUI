using System.Collections;
using System.Collections.Generic;
using Lib.Sound;
using UnityEngine;

public class StartSignal : MonoBehaviour
{
    // 編集可能
    [SerializeField,Header("Readyが表示されるまでの時間")]
    float CounterForDisplayReady = 1.0f;
    [SerializeField, Header("ReadyからGoが表示されるまでの時間")]
    float CounterForDisplayGo = 2.0f;
    [SerializeField, Header("Goからゲームが始まるまでの時間")]
    float CounterForGameStart = 1.0f;

    // private
    GameObject ObjReady;
    GameObject ObjStart;
    float SignalTimer;
    float PrevSignalTimer;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.BGM.Stop();
        ObjReady = GameObject.Find("Ready");
        ObjStart = GameObject.Find("Go");

        ObjReady.SetActive(false);
        ObjStart.SetActive(false);

        SignalTimer = 0;
        PrevSignalTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float timeDispReady = CounterForDisplayReady;
        float timeDispGo = CounterForDisplayReady + CounterForDisplayGo;
        float timeStart = CounterForDisplayReady + CounterForDisplayGo + CounterForGameStart;

        if(SignalTimer > timeStart)
        {
            if(PrevSignalTimer <= timeStart)
            {
                SoundManager.Instance.BGM.PlayCrossFade(_BGM._TITLE_BGM);
            }
            ObjReady.SetActive(false);
            ObjStart.SetActive(false);
        }
        else if(SignalTimer > timeDispGo)
        {
            if(PrevSignalTimer <= timeDispGo)
            {
                SoundManager.Instance.Jingle.Play(_Jingle._GO);
            }
            ObjReady.SetActive(false);
            ObjStart.SetActive(true);
        }
        else if(SignalTimer > timeDispReady)
        {
            if(PrevSignalTimer <= timeDispReady)
            {
                SoundManager.Instance.Jingle.Play(_Jingle._LADY);
            }
            ObjReady.SetActive(true);
            ObjStart.SetActive(false);
        }
        else
        {
            ObjReady.SetActive(false);
            ObjStart.SetActive(false);
        }

        // Count Timer
        PrevSignalTimer = SignalTimer;
        SignalTimer += Time.deltaTime;
    }
}
