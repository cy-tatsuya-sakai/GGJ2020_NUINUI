using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

/// <summary>
/// なんかマチ針を保持。三角形を作るためのリスト。その他計算
/// </summary>
public class MarkingPinManager : MonoBehaviour
{
    [SerializeField] private MarkingPin _markingPinPrefab;
    [SerializeField] private Patchwork  _patchworkPrefab;

    private List<MarkingPin>    _markingPinList = new List<MarkingPin>();
    private List<Patchwork>     _patchworkList = new List<Patchwork>();

    public void Init()
    {
        _markingPinList.Clear();    // 意味ない
        _patchworkList.Clear();
    }

    /// <summary>
    /// 針を追加
    /// </summary>
    /// <param name="pos"></param>
    public void AddPin(Vector3 pos)
    {
        var obj = Instantiate(_markingPinPrefab, pos, Quaternion.identity);
        obj.transform.SetParent(transform);
        _markingPinList.Add(obj);
        obj.Init(_markingPinList.Count - 1);
    }

    /// <summary>
    /// 三角形を作る
    /// </summary>
    public void CreatePatchwork()
    {
        int num = _markingPinList.Count;
        if(num < 4) { return; }
        var a = _markingPinList[num - 1].transform.position;
        var b = _markingPinList[num - 2].transform.position;
        var c = _markingPinList[num - 3].transform.position;
        var d = _markingPinList[num - 4].transform.position;

        
        var ad = (a - d).sqrMagnitude;  // 1点目と4点目が十分近いならきっと三角形
        if(ad <= 0.001f)
        {
            var ab = a - b;
            var ac = a - c;
            var nn = new Vector3(-ab.y, ab.x, ab.z).normalized;
            if(Mathf.Abs(Vector3.Dot(nn, ac)) > 0.001f)
            {
                // 多少高さもあるみたいだし、多分三角形…
                CreatePatchwork_(a, b, c);
                ClearPin();
            }
        }
    }

    private void CreatePatchwork_(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 pos = (a + b + c) / 3.0f;
        a = a - pos;
        b = b - pos;
        c = c - pos;
        var obj = Instantiate(_patchworkPrefab, pos, Quaternion.identity);
        obj.transform.SetParent(transform);
        _patchworkList.Add(obj);
        obj.Init(a, b, c);
    }

    private void ClearPin()
    {
        int num = _markingPinList.Count;
        for(int i = 0; i < num; i++)
        {
            Destroy(_markingPinList[i].gameObject);
        }
        _markingPinList.Clear();
    }

    /// <summary>
    /// クリックした位置のマチ針があれば返す。なければnull
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public MarkingPin GetMarkingPin(Vector3 pos)
    {
        var collList = Physics2D.OverlapPointAll(new Vector2(pos.x, pos.y));
        Debug.Log(collList.Length);
        int idx = -1;
        foreach(var coll in collList)
        {
            var pin = coll.gameObject.GetComponentInParent<MarkingPin>();
            if(pin == null) { continue; }

            if(pin.Index > idx)
            {
                idx = pin.Index;
            }
        }

        if(idx < 0 || idx >= _markingPinList.Count)
        {
            return null;
        }

        return _markingPinList[idx];
    }

    /// <summary>
    /// パッチワークを実行。衝突した敵とか穴とかを返す
    /// </summary>
    public List<EnemyCollision> ExecPatchwork()
    {
        List<EnemyCollision> enemyList = new List<EnemyCollision>();
        foreach(var patchwork in _patchworkList)
        {
            enemyList.AddRange(patchwork.CollisionEnemy());
        }

        int num = _patchworkList.Count;
        for(int i = 0; i < num; i++)
        {
            _patchworkList[i].transform.DOScale(new Vector3(0.0f, 0.0f, 1.0f), 0.25f).SetEase(Ease.InBack);
            Destroy(_patchworkList[i].gameObject, 1.0f);
        }
        _patchworkList.Clear();

        enemyList.Distinct();
        return enemyList;
    }
}
