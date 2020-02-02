using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private float _speed, _timer;
    [SerializeField,Header("穴の最大サイズ")] private float maxSize;

    // Game Level
    private int _gameLevel;
    private int _gameLevelMax;

    // Start is called before the first frame update
    void Start()
    {
        //スピードによって拡大速度を変更
        _speed = Random.Range(5.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        //穴のサイズを徐々に大きくする
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x + Time.deltaTime / _speed, 1.0f, maxSize),
                                                                 Mathf.Clamp(transform.localScale.y + Time.deltaTime / _speed, 1.0f, maxSize));

        if (_timer >= 3.0f)
        {
            Destroy(this);
        }
    }

    // Set Game Level
    public void SetGameLevel(int level, int levelMax)
    {
        _gameLevel = level;
        _gameLevelMax = levelMax;
    }
}
