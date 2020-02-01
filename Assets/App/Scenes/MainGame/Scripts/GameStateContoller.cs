using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateContoller : MonoBehaviour
{
    [SerializeField, Header("シーン開始からゲームが始まるまでの時間")]
    float WaitForGameStart = 4.0f;
    [SerializeField, Header("表面の時間")]
    float GameTimeMax = 120;
    [SerializeField, Header("裏面の時間")]
    float GameReverseTimeMax = 120;
    [SerializeField, Header("コンボ期間に入るコンボ数")]
    int ComboTermNumMax = 3;
    [SerializeField, Header("エネミーPrefab")]
    GameObject objEnemy;

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
    bool isReverse;
    float startReverseCounter;
    float gameReverseCounter;

    // Combo Term Control
    bool isComboTerm;
    float comboCounter;
    [SerializeField, Header("コンボ期間（穴の抑制）")]
    float comboCounterMax = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        gameStatus = GameStatus.Ready;
        startCounter = 0.0f;
        gameCounter = 0.0f;
        isReverse = false;
        startReverseCounter = 0.0f;
        gameReverseCounter = 0.0f;

        isComboTerm = false;
        comboCounter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Start Counter
        if(isReverse)
        {
            startReverseCounter += Time.deltaTime;
            if (startReverseCounter > WaitForGameStart)
            {
                gameStatus = GameStatus.GameReverse;
            }
        }
        else
        {
            startCounter += Time.deltaTime;
            if (startCounter > WaitForGameStart)
            {
                gameStatus = GameStatus.Game;
            }
        }

        // Game Counter
        if(isReverse)
        {
            if (gameStatus == GameStatus.GameReverse)
            {
                gameReverseCounter += Time.deltaTime;
                // タイムアップ？
                if(gameReverseCounter > GameReverseTimeMax)
                {
                    SceneManager.LoadScene("Title");
                }
            }
        }
        else
        {
            if (gameStatus == GameStatus.Game)
            {
                gameCounter += Time.deltaTime;
                // 裏になる？
                if(gameCounter > GameTimeMax)
                {
                    isReverse = true;
                    gameStatus = GameStatus.ReadyReverse;
                }
            }
        }

        // Combo Counter
        if (isComboTerm)
        {
            comboCounter += Time.deltaTime;
            Debug.Log(comboCounter);
            // コンボ終了？
            if (comboCounter > comboCounterMax)
            {
                comboCounter = 0.0f;
                isComboTerm = false;

                // 敵を増やす
                Instantiate(objEnemy, new Vector3(0.0f, 0.0f, -1.0f), Quaternion.identity);
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
        float remainCounter = GameTimeMax - gameCounter;
        return remainCounter;
    }

    // 裏面タイマー参照
    public float GetGameReverseCounter()
    {
        float remainCounter = GameReverseTimeMax - gameReverseCounter;
        return remainCounter;
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

    // 裏面ですか？
    public bool IsReverse()
    {
        return isReverse;
    }
}
