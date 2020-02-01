using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Lib.Sound;

/// <summary>
/// 倒した敵ごとのスコア表示
/// </summary>
public class ScorePopup : MonoBehaviour
{
    private static readonly string[] comboName = new string[] { "GOOD", "NICE", "EXCELLENT"};
    private static Color[] comboColor;
    private static readonly float[] comboScale = new float[] { 1.0f, 1.2f, 1.5f };

    [SerializeField] private GameObject _particlePrefab;

    private static readonly string[] comboSE = new string[]
    {
        _SE._COMBO1,
        _SE._COMBO2,
        _SE._COMBO3,
        _SE._COMBO4,
        _SE._COMBO5,
        _SE._COMBO6,
        _SE._COMBO7,
        _SE._COMBO8,
        _SE._COMBO9,
    };

    void Awake()
    {
        Color a, b, c;
        ColorUtility.TryParseHtmlString("#5AFD1C", out a);
        ColorUtility.TryParseHtmlString("#FFD91C", out b);
        ColorUtility.TryParseHtmlString("#FDA631", out c);

        // 毎回設定しちゃう！
        comboColor = new Color[]
        {
            a, b, c
        };
    }

    [SerializeField] private TextMeshProUGUI _text;

    public void Popup(int comboCount)
    {
        SoundManager.Instance.SE.Play(comboSE[Mathf.Clamp(comboCount, 0, comboSE.Length - 1)]);

        // Excellentとかの回数を保存しておく。こんな末端で……！
        int idx = Mathf.Clamp(comboCount, 0, 2);
        if(idx == 0)
        {
            MainGameScore.good++;
        }
        else if(idx == 1)
        {
            MainGameScore.nice++;
        }
        else
        {
            MainGameScore.excellent++;

            float a = 1920.0f / 1080.0f;
            float y = 26.0f;
            float x = y * a;
            for(int i = 0; i < 10; i++)
            {
                var pos = new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0.0f);
                if(Mathf.Abs(pos.x) < 20.0f) { pos.x = Mathf.Sign(x) * 20.0f; }
                if(Mathf.Abs(pos.y) < 10.0f) { pos.y = Mathf.Sign(y) * 10.0f; }
                var obj = Instantiate(_particlePrefab, pos, Quaternion.identity);
                Destroy(obj.gameObject, 1.0f);
            }
        }

        _text.text = comboName[Mathf.Clamp(comboCount, 0, comboName.Length - 1)];
        _text.color = comboColor[Mathf.Clamp(comboCount, 0, comboColor.Length - 1)];

        float scale = comboScale[Mathf.Clamp(comboCount, 0, comboScale.Length - 1)];
        transform.DOMoveY(transform.position.y + 50.0f, 0.1f);
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * scale * 0.75f, 0.1f);
        Destroy(gameObject, 1.5f);
    }
}
