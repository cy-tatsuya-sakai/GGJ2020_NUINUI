using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private float _speed, _timer;

    // Start is called before the first frame update
    void Start()
    {
        _speed = Random.Range(1.5f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        //穴のサイズを徐々に大きくする
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x + Time.deltaTime / _speed, 1.0f, 5.0f),
                                                                 Mathf.Clamp(transform.localScale.y + Time.deltaTime / _speed, 1.0f, 5.0f));

        if (_timer >= 3.0f)
        {
            Destroy(this);
        }
    }
}
