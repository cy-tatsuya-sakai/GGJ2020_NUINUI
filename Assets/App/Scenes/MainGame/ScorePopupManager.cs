using System.Collections;
using System.Collections.Generic;
using Lib.Util;
using UnityEngine;

/// <summary>
/// なんかスコア数字がポップアップする。コンボとかもここ…？
/// </summary>
public class ScorePopupManager : MonoBehaviour
{
    private class Info
    {
        public Vector3 pos;
        public int score;
        public int comboCount;

        public Info(Vector3 pos, int score, int comboCount)
        {
            this.pos = pos;
            this.score = score;
            this.comboCount = comboCount;
        }
    }

    [SerializeField] private Canvas     _canvas;
    [SerializeField] private ScorePopup _scorePopupPrefab;

    private SimpleTimer _timer = new SimpleTimer();
    private List<Info> _popupList = new List<Info>();
    private int _comboCount = 0;    // 毎フレームクリアする。コンボを内部でカウント

    public void PopupScore(int score, Vector3 pos, Camera cam)
    {
        // score実質使わなくなってる
        var v = RectTransformUtility.WorldToScreenPoint(cam, pos);
        _popupList.Add(new Info(v, score, _comboCount++));
        _timer.Init(0);

        /*
        var v = RectTransformUtility.WorldToScreenPoint(cam, pos);
        var obj = Instantiate(_scorePopupPrefab, _canvas.transform);
        obj.transform.position = v;
        obj.Popup(score);
        */
    }

    void Update()
    {
        _comboCount = 0;

        if(_popupList.Count <= 0) { return; }
        if(_timer.Update() == false) { return; }
        var info = _popupList[0];
        Popup_(info);
        _popupList.Remove(info);
    }

    private void Popup_(Info info)
    {
        var obj = Instantiate(_scorePopupPrefab, _canvas.transform);
        obj.transform.position = info.pos;
        obj.Popup(info.comboCount);
        //StartCoroutine(Slow());
        _timer.Init(0.1f);
    }

    private IEnumerator Slow()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 1.0f;
    }
}
