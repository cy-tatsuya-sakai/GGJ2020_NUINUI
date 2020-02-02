using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private float _speed, _timer;
    [SerializeField, Header("穴の大きさの上限値")] private float maxSize;
    [SerializeField,Header("穴の拡大速度の下限値")] private float speedMin;
    [SerializeField, Header("穴の拡大速度の上限値")] private float speedMax;

    // Game Level
    private int _gameLevel;
    private int _gameLevelMax;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameLevel : " + _gameLevel);

        //スピードによって拡大速度を変更
        if(_gameLevel == _gameLevelMax)
        {
            _speed = Random.Range(speedMin * (_gameLevelMax * 1.75f), speedMax * (_gameLevelMax * 1.75f));
        }
        else if (_gameLevel == 0)
        {
            _speed = Random.Range(speedMin, speedMax);
        }
        else if(_gameLevel >= 1 && _gameLevel <= 3)
        {
            _speed = Random.Range(speedMin * (_gameLevel * 1.25f), speedMax * (_gameLevel * 1.25f));
        }
        else if(_gameLevel >= 4 && _gameLevel < 5)
        {
            _speed = Random.Range(speedMin * (_gameLevel * 1.5f), speedMax * (_gameLevel * 1.5f));
        }

    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        //穴のサイズを徐々に大きくする
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x + Time.deltaTime * _speed, 1.0f, maxSize),
                                                                 Mathf.Clamp(transform.localScale.y + Time.deltaTime * _speed, 1.0f, maxSize));

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
