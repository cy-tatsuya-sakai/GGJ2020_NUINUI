using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib.Util;
using Lib.Sound;

public class MarkingPinTest : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject testObj;
    [SerializeField] private MarkingPinManager _markingPinManagerPrefab;
    [SerializeField] private ScorePopupManager _scorePopupManagerPrefab;

    private MarkingPinManager _markingPinManager;
    private ScorePopupManager _scorePopupManager;

    private StateMachine _state;

    // Game Manager
    GameObject objGameManager;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.StopAll();

        _markingPinManager = Instantiate(_markingPinManagerPrefab);
        _scorePopupManager = Instantiate(_scorePopupManagerPrefab);
        _state = new StateMachine(State_Normal);

        // Game Manager
        objGameManager = GameObject.Find("GameManager");
        objGameManager.GetComponent<GameStateContoller>().SetMarkingPinManager(_markingPinManager);
    }

    // Update is called once per frame
    void Update()
    {
        _state.Update();
    }

    private void State_Normal(StateMachine.Case c)
    {
        switch(c)
        {
            case StateMachine.Case.Exec:
            {
                testObj.transform.position = Util.GetMouseWorldPosition(_cam);

                if(Input.GetMouseButtonDown(0) && HitDress(testObj.transform.position))
                {
                    GameStateContoller.GameStatus status = objGameManager.GetComponent<GameStateContoller>().GetGameStatus();
                    if(status == GameStateContoller.GameStatus.Ready
                        || status == GameStateContoller.GameStatus.ReadyReverse)
                    {

                    }
                    else
                    {
                        var pin = _markingPinManager.GetMarkingPin(testObj.transform.position);
                        var pos = (pin == null) ? testObj.transform.position : pin.transform.position;
                        _markingPinManager.AddPin(pos);

                        // 何かに使えるかもしれないパッチ三角形の面積
                        var areaSize = _markingPinManager.CreatePatchwork();
                        
                        //Debug.LogWarning(areaSize);
                    }
                }

                if(Input.GetMouseButtonDown(1))
                {
                    var list = _markingPinManager.ExecPatchwork();
                    int num = list.Count;
                    // Game Manager
                    objGameManager.GetComponent<GameStateContoller>().SetComboTerm(num);

                    for (int i = 0; i < num; i++)
                    {
                        _scorePopupManager.PopupScore(100/*適当。後で文字になるかもとのこと*/, list[i].transform.position, _cam);

                        //Enemyが三角形に入っていたら動きを一時停止
                        if (list[i].gameObject.GetComponent<Enemy>())
                        {
                            var enemyScr = list[i].gameObject.GetComponent<Enemy>();
                                if (enemyScr.reStart == false)
                                {
                                    enemyScr.reStart = true;
                                }    
                            }
                            
                        else
                        {
                            Destroy(list[i].gameObject);
                        }
                    }
                }
            }
            break;
        }
    }

    private bool HitDress(Vector3 pos)
    {
        var hitList = Physics.RaycastAll(pos - Vector3.forward * 10, Vector3.forward, 100.0f);
        foreach(var hit in hitList)
        {
            if(hit.collider.gameObject.GetComponentInParent<Dress>() != null)
            {
                return true;
            }
        }

        return false;
    }
}
