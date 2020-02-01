using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateContoller : MonoBehaviour
{
    [SerializeField, Header("シーン開始からゲームが始まるまでの時間")]
    float WaitForGameStart = 4.0f;
    [SerializeField, Header("表面の時間")]
    float GameTimeMax = 30;
    [SerializeField, Header("裏面の時間")]
    float GameReverseTimeMax = 30;
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
        Timeup,
    }

    // private
    GameStatus gameStatus;
    float startCounter;
    float gameCounter;
    bool isReverse;
    float startReverseCounter;
    float gameReverseCounter;
    float timeupCounter;
    [SerializeField, Header("タイムアップ表示時間")]
    float timeupCounterMax = 2.0f;

    // Combo Term Control
    bool isComboTerm;
    float comboCounter;
    [SerializeField, Header("コンボ期間（穴の抑制）")]
    float comboCounterMax = 5.0f;

    // Result
    [SerializeField, Header("Bad Endになる穴の数")]
    int numberOfBadHoles = 2;
    [SerializeField, Header("Excellent EndになるExc.の数")]
    int numberOfExcellents = 20;
    int numOfExcellent;
    int numOfHoles;
    GameObject objTimeup;

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

        numOfExcellent = 0;
        numOfHoles = 0;

        objTimeup = GameObject.Find("Canvas Timeup");
        objTimeup.SetActive(false);
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
                    numOfExcellent = MainGameScore.excellent;
                    numOfHoles = GetNumberOfHoles();
                    gameStatus = GameStatus.Timeup;
                    objTimeup.SetActive(true);
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
            // コンボ終了？
            if (comboCounter > comboCounterMax)
            {
                comboCounter = 0.0f;
                isComboTerm = false;

                // 敵を増やす
                Instantiate(objEnemy, new Vector3(0.0f, 0.0f, -1.0f), Quaternion.identity);
            }
        }

        // Timeup Counter
        if(gameStatus == GameStatus.Timeup)
        {
            timeupCounter += Time.deltaTime;
            if (timeupCounter > timeupCounterMax)
            {
                if (numOfExcellent >= numberOfExcellents)
                {
                    SceneManager.LoadScene("EndingExcellent");
                }
                else if (numOfHoles <= numberOfBadHoles)
                {
                    SceneManager.LoadScene("EndingGood");
                }
                else
                {
                    SceneManager.LoadScene("EndingBad");
                }
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
        Debug.Log(MainGameScore.good);
        Debug.Log(MainGameScore.nice);
        Debug.Log(MainGameScore.excellent);
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

    // 穴の数を返す
    int GetNumberOfHoles()
    {
        int holes = 0;

        return holes;
    }

    // エネミーと穴と線をリセット
    void ResetGameObject()
    {

    }

    // エネミー再配置
    void CreateEnemyForReverse()
    {

    }
}
