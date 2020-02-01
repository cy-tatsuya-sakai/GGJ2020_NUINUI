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
    [SerializeField] private TextMeshProUGUI _text;

    public void Popup(int score)
    {
        _text.text = score.ToString();
        transform.DOMoveY(transform.position.y + 50.0f, 0.1f);
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * 1.0f, 0.1f);
        Destroy(gameObject, 1.5f);
    }
}
