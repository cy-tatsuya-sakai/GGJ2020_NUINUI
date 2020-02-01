using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateContoller : MonoBehaviour
{
    [SerializeField, Header("シーン開始からゲームが始まるまでの時間")]
    float WaitForGameStart = 4.0f;
    [SerializeField, Header("裏シーン開始からエネミーが出現するまでの時間")]
    float WaitForCreateEnemyReverse = 2.0f;
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
    bool isCreatedEnemies;
    [SerializeField, Header("再配置するエネミーの数")]
    int numberOfEnemiesForReverse = 3;
    [SerializeField, Header("再配置するエネミーの範囲")]
    float createEnemySpan = 5.0f;
    float startReverseCounter;
    float gameReverseCounter;
    float timeupCounter;
    [SerializeField, Header("タイムアップ表示時間")]
    float timeupCounterMax = 3.0f;

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

    // for delete lines
    MarkingPinManager markingPinManager;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = GameStatus.Ready;
        startCounter = 0.0f;
        gameCounter = 0.0f;
        isReverse = false;
        isCreatedEnemies = false;
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
        if(gameStatus == GameStatus.Ready)
        {
            startCounter += Time.deltaTime;
            if (startCounter > WaitForGameStart)
            {
                gameStatus = GameStatus.Game;
            }
        }else if (gameStatus == GameStatus.ReadyReverse)
        {
            startReverseCounter += Time.deltaTime;
            if (startReverseCounter > WaitForGameStart)
            {
                gameStatus = GameStatus.GameReverse;
            }

            // エネミー生成
            if(startReverseCounter > WaitForCreateEnemyReverse)
            {
                if (isCreatedEnemies == false)
                {
                    CreateEnemyForReverse();
                    isCreatedEnemies = true;
                }
            }
        }

        // Game Counter
        if (gameStatus == GameStatus.Game)
        {
            gameCounter += Time.deltaTime;
            // 裏になる？
            if (gameCounter > GameTimeMax)
            {
                isReverse = true;
                gameStatus = GameStatus.ReadyReverse;
                // エネミーと穴と線をリセット
                ResetGameObject();
            }
        }
        else if (gameStatus == GameStatus.GameReverse)
        {
            gameReverseCounter += Time.deltaTime;
            // タイムアップ？
            if (gameReverseCounter > GameReverseTimeMax)
            {
                numOfExcellent = MainGameScore.excellent;
                numOfHoles = GetNumberOfHoles();
                gameStatus = GameStatus.Timeup;
                objTimeup.SetActive(true);
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
                if(numOfHoles > numberOfBadHoles)
                {
                    SceneManager.LoadScene("EndingBad");
                }
                else if (numOfExcellent >= numberOfExcellents)
                {
                    SceneManager.LoadScene("EndingExcellent");
                }
                else
                {
                    SceneManager.LoadScene("EndingGood");
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
        GameObject[] tagObjects;
        tagObjects = GameObject.FindGameObjectsWithTag("Hole");
        holes = tagObjects.Length;

        Debug.Log("Number of Holes:");
        Debug.Log(holes);

        return holes;
    }

    // エネミーと穴と線をリセット
    void ResetGameObject()
    {
        // エネミーを消す
        ResetEnemies();

        // 穴を消す
        ResetHoles();

        // 線を消す
        ResetLines();
    }

    // エネミーを消す
    void ResetEnemies()
    {
        GameObject[] tagObjects;
        tagObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject tagObj in tagObjects)
        {
            // 消す！
            Destroy(tagObj);
        }

    }

    // 穴を消す
    void ResetHoles()
    {
        GameObject[] tagObjects;
        tagObjects = GameObject.FindGameObjectsWithTag("Hole");
        foreach (GameObject tagObj in tagObjects)
        {
            // 消す！
            Destroy(tagObj);
        }
    }

    // 線を消す
    void ResetLines()
    {
        markingPinManager.ExecPatchwork();
    }

    // 先の元を設定
    public void SetMarkingPinManager(MarkingPinManager marking)
    {
        markingPinManager = marking;
    }

    // エネミー再配置
    void CreateEnemyForReverse()
    {
        for(int i=0; i<numberOfEnemiesForReverse;++i)
        {
            float x = Random.Range(-createEnemySpan, createEnemySpan);
            float y = Random.Range(-createEnemySpan, createEnemySpan);
            // 敵を増やす
            Instantiate(objEnemy, new Vector3(x, y, -1.0f), Quaternion.identity);
        }
    }
}
