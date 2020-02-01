using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateContoller : MonoBehaviour
{
    [SerializeField, Header("シーン開始からゲームが始まるまでの時間")]
    float WaitForGameStart = 4.0f;
    [SerializeField, Header("表面の時間")]
    float GameTimeMax;
    [SerializeField, Header("コンボ期間に入るコンボ数")]
    int ComboTermNumMax = 3;

    // public
    public enum GameStatus
    {
        Ready,
        Game,
        ReadyReverse,
        GameReverse,
    }

    // private
    GameStatus gameStatus;
    float startCounter;
    float gameCounter;

    // Combo Term Control
    bool isComboTerm;
    float comboCounter;
    [SerializeField, Header("コンボ期間（穴の抑制）")]
    float comboCounterMax = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        gameStatus = GameStatus.Ready;
        startCounter = 0.0f;
        gameCounter = 0.0f;

        isComboTerm = false;
        comboCounter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Start Counter
        startCounter += Time.deltaTime;
        if(startCounter > WaitForGameStart)
        {
            gameStatus = GameStatus.Game;
        }

        // Game Counter
        if( gameStatus == GameStatus.Game
            || gameStatus == GameStatus.GameReverse)
        {
            gameCounter += Time.deltaTime;
        }

        // Combo Counter
        if (isComboTerm)
        {
            comboCounter += Time.deltaTime;
            if (comboCounter > comboCounterMax)
            {
                comboCounter = 0.0f;
                isComboTerm = false;
            }
        }
    }

    // ゲームステータス参照
    public GameStatus GetGameStatus()
    {
        return gameStatus;        
    }

    // タイマー参照
    public float GetGameCounter()
    {
        return gameCounter;
    }

    // コンボ期間中？
    public bool IsComboTerm()
    {
        return isComboTerm;
    }

    // コンボ期間に入ります
    public void SetComboTerm(int num)
    {
        Debug.Log("ComboNum:");
        Debug.Log(num);
        if (num >= ComboTermNumMax)
        {
            isComboTerm = true;
            comboCounter = 0.0f;
        }
    }
}
