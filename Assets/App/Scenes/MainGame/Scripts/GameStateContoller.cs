using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateContoller : MonoBehaviour
{
    [SerializeField, Header("シーン開始からゲームが始まるまでの時間")]
    float WaitForGameStart = 4.0f;
    [SerializeField, Header("表面の時間")]
    float GameTimeMax;

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

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = GameStatus.Ready;
        startCounter = 0.0f;
        gameCounter = 0.0f;
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
}
