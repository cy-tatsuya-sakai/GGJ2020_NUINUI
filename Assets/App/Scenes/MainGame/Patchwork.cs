using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// マチ針を結んでできた三角形
/// </summary>
public class Patchwork : MonoBehaviour
{
    [SerializeField] private GameObject line1;
    [SerializeField] private GameObject line2;
    [SerializeField] private GameObject line3;
    [SerializeField] private PolygonCollider2D _polyCollider;

    private List<EnemyCollision>    _enemyList = new List<EnemyCollision>();

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    public void Init(Vector3 a, Vector3 b, Vector3 c)
    {
        // 仮
        a.z = -0.6f;
        b.z = -0.6f;
        c.z = -0.6f;
        //
        line1.transform.position = a;
        line2.transform.position = b;
        line3.transform.position = c;

        var ba = b - a;
        var cb = c - b;
        var ac = a - c;

        line1.transform.rotation = Quaternion.LookRotation(ba, Vector3.up);
        line2.transform.rotation = Quaternion.LookRotation(cb, Vector3.up);
        line3.transform.rotation = Quaternion.LookRotation(ac, Vector3.up);
        line1.transform.localScale = new Vector3(0.1f, 0.1f, ba.magnitude);
        line2.transform.localScale = new Vector3(0.1f, 0.1f, cb.magnitude);
        line3.transform.localScale = new Vector3(0.1f, 0.1f, ac.magnitude);

        var path = new Vector2[] {
            new Vector2(a.x, a.y),
            new Vector2(b.x, b.y),
            new Vector2(c.x, c.y)
        };
        _polyCollider.SetPath(0, path);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // 一度でも当たった敵は、後々の衝突判定候補にする
        var a = coll.gameObject.GetComponent<EnemyCollision>();
        if(a == null) { return; }
        if(_enemyList.Any(x => x == a)) { return; }
        _enemyList.Add(a);
    }

    public List<EnemyCollision> CollisionEnemy()
    {
        var a = line1.transform.position;
        var b = line2.transform.position;
        var c = line3.transform.position;
        var ba = b - a;
        var cb = c - b;
        var ac = a - c;

        var list = new List<EnemyCollision>();
        foreach(var enemy in _enemyList)
        {
            if(enemy == null) { continue; }
            
            // 敵が三角形に含まれている。完全に含まれているが条件なので、とりあえず中心点のみチェック
            {
                var hit = Physics2D.OverlapPointAll(new Vector2(enemy.transform.position.x, enemy.transform.position.y));
                if(hit.Any(x => x == _polyCollider) == false)
                {
                    continue;
                }
            }

            // 三角系の辺と敵が接触しているか。接触していなければ、完全に内包している
            {
                var hit = Physics2D.LinecastAll(a, b).Select(x => x.collider.gameObject.GetComponent<EnemyCollision>()).ToList();
                if(hit.Any(x => x == enemy))
                {
                    continue;
                }
            }
            {
                var hit = Physics2D.LinecastAll(b, c).Select(x => x.collider.gameObject.GetComponent<EnemyCollision>()).ToList();
                if(hit.Any(x => x == enemy))
                {
                    continue;
                }
            }
            {
                var hit = Physics2D.LinecastAll(c, a).Select(x => x.collider.gameObject.GetComponent<EnemyCollision>()).ToList();
                if(hit.Any(x => x == enemy))
                {
                    continue;
                }
            }

            list.Add(enemy);
        }

        return list;
    }
}
