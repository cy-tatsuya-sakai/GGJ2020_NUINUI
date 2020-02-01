using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

/// <summary>
/// 倒した敵ごとのスコア表示
/// </summary>
public class ScorePopup : MonoBehaviour
{
    private static readonly string[] comboName = new string[] { "GOOD", "NICE", "EXCELLENT"};
    private static readonly Color[] comboColor;
    private static readonly float[] comboScale = new float[] { 1.0f, 1.2f, 1.5f };

    static ScorePopup()
    {
        Color a, b, c;
        // ColorUtility.TryParseHtmlString("#5AFD1C", out a);
        // ColorUtility.TryParseHtmlString("#FFD91C", out b);
        // ColorUtility.TryParseHtmlString("#FF1C23", out c);

        comboColor = new Color[]
        {
            Color.green, Color.yellow, Color.red
        };
    }

    [SerializeField] private TextMeshProUGUI _text;

    public void Popup(int comboCount)
    {
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
        }

        _text.text = comboName[Mathf.Clamp(comboCount, 0, comboName.Length - 1)];
        _text.color = comboColor[Mathf.Clamp(comboCount, 0, comboColor.Length - 1)];

        float scale = comboScale[Mathf.Clamp(comboCount, 0, comboScale.Length - 1)];
        transform.DOMoveY(transform.position.y + 50.0f, 0.1f);
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * scale, 0.1f);
        Destroy(gameObject, 1.5f);
    }
}
