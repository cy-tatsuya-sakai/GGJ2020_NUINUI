using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib.Util;

public class MarkingPinTest : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject testObj;
    [SerializeField] private MarkingPinManager _markingPinManagerPrefab;
    [SerializeField] private ScorePopupManager _scorePopupManagerPrefab;

    private MarkingPinManager _markingPinManager;
    private ScorePopupManager _scorePopupManager;

    private StateMachine _state;

    // Start is called before the first frame update
    void Start()
    {
        _markingPinManager = Instantiate(_markingPinManagerPrefab);
        _scorePopupManager = Instantiate(_scorePopupManagerPrefab);
        _state = new StateMachine(State_Normal);
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

                if(Input.GetMouseButtonDown(0))
                {
                    var pin = _markingPinManager.GetMarkingPin(testObj.transform.position);
                    var pos = (pin == null) ? testObj.transform.position : pin.transform.position;
                    _markingPinManager.AddPin(pos);

                    _markingPinManager.CreatePatchwork();
                }

                if(Input.GetMouseButtonDown(1))
                {
                    var list = _markingPinManager.ExecPatchwork();
                    int num = list.Count;
                    for(int i = 0; i < num; i++)
                    {
                        _scorePopupManager.PopupScore(100/*適当。後で文字になるかもとのこと*/, list[i].transform.position, _cam);
                        Destroy(list[i].gameObject);
                    }
                }
            }
            break;
        }
    }
}
