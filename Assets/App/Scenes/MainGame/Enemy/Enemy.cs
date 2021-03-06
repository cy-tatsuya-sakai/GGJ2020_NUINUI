﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Ray _ray;
    //ヒットしたオブジェクト情報
    private RaycastHit _hit;
    [SerializeField] private float raydis;
    //rayがオブジェクト(服)に当たっているかの判定
    public bool _rayHit;

    //Enemyの移動ポイント
    [SerializeField] private List<GameObject> enemyRoots;
    [SerializeField, Header("Enemyの移動速度")] private float speed;
    private int _rootNum, _beforeRootNum, _canNotGoRootNum;

    public bool moveStop, reStart;

    private float _timer, _randomTime, _numbnessTimer;

    [SerializeField,Header("穴オブジェクト")] private GameObject hole;
    private bool instance;

    [SerializeField,Header("Enemyの移動再開時間")] private int reStartTime;

    [SerializeField] private bool restartReset;

    // Game Manager
    GameObject objGameManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRoots = new List<GameObject>();
        //EnemyRootを取得
        var enemyRoot = GameObject.Find("EnemyRoot");
        //EnemyRootの子オブジェクトを全取得、配列に格納
        foreach (Transform childTransform in enemyRoot.transform)
        {
            enemyRoots.Add(childTransform.gameObject);
        }

        _rayHit = true;
        //移動地点の設定
        _rootNum = Random.Range(0, enemyRoots.Count);
        //穴生成時間の設定
        _randomTime = Random.Range(0.0f, 5.0f);
        // Game Manager
        objGameManager = GameObject.Find("GameManager");

        restartReset = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RootPoint"))
        {
            RootSetting(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var enemyPos = transform.position;

        _ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        //Rayがhitしなかったら移動先を再設定
        if (Physics.Raycast(_ray, out _hit, raydis) == false)
        {
            RootSetting(2);
        }

        if (reStart)
        {
            if(restartReset == false)
            {
                _numbnessTimer = 5.0f;
                restartReset = true;
            }

            transform.position = enemyPos;
            _numbnessTimer -= Time.deltaTime;

           if (_numbnessTimer >= 0.0f)
            {
                enemyPos = transform.position;
            }
            else
            {
                restartReset = false;
                reStart = false;
                //ReStartEnemy();
            }
        }
        else
        {
            //タイマー開始
            _timer += Time.deltaTime;
            //タイマーがrandomTimeを超えていたら移動停止
            if (_timer >= _randomTime)
            {
                moveStop = true;
            }

            switch (moveStop)
            {
                case true:
                    //Enemyの動きを停止
                    transform.position = enemyPos;

                    //穴を生成
                    bool isComboTerm = objGameManager.GetComponent<GameStateContoller>().IsComboTerm();
                    if (instance == false && isComboTerm == false)
                    {
                        CreateHole(enemyPos);
                    }
                    break;

                case false:
                    //rootPointに向けてEnemyを移動
                    transform.position = Vector3.MoveTowards(transform.position, enemyRoots[_rootNum].transform.position, speed * Time.deltaTime);
                    instance = false;

                    //もしEnemyの動きが止まってしまっていたらルートを再設定
                    if (_timer >= _randomTime && transform.position == enemyRoots[_rootNum].transform.position)
                    {
                        RootSetting(1);
                    }
                    break;
            }
        }
    }

    void RootSetting(int type)
    {
        switch (type)
        {
            case 1:
                _beforeRootNum = _rootNum;

                int max = enemyRoots.Count;
                int[] box = new int[max];

                int index = 0;
                for(int i = 0; i < max; i++)
                {
                    if(i != _rootNum)
                    {
                        box[index] = i;
                        index++;
                    }
                }

                var setNum = Random.Range(0, enemyRoots.Count - 1);
                _rootNum = box[setNum];
                if(_beforeRootNum == _rootNum)
                {
                    Debug.Log("Same Value. _rootNum _beforeRootNum");
                    Debug.Log(_beforeRootNum);
                    Debug.Log(_rootNum);

                }
                break;

            case 2:
                _rayHit = false;
                //そのrootPointには次の移動では行けないようにする
                _canNotGoRootNum = _rootNum;
                //ひとつ前のrootPointに戻る
                _rootNum = _beforeRootNum;
                break;
        }
    }

    void CreateHole(Vector3 enemyPos)
    {
        var createHole = Instantiate(hole);
        createHole.transform.position = new Vector3(enemyPos.x, enemyPos.y, -0.55f);
        int level = objGameManager.GetComponent<GameStateContoller>().GetGameLevel();
        int levelMax = objGameManager.GetComponent<GameStateContoller>().GetGameLevelMax();
        createHole.GetComponent<Hole>().SetGameLevel(level,levelMax);
        instance = true;
        Invoke("ReStartEnemy", 3.0f);
    }


    void ReStartEnemy()
    {
        //タイマーをリセット
        _timer = 0.0f;
        _randomTime = Random.Range(3.0f, 5.0f);

        //移動開始
        moveStop = false;
    }
}
