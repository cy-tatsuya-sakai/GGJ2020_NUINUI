using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private Enemy _enemy;
    private float _maxSize;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = transform.root.gameObject.GetComponent<Enemy>();
        _maxSize = Random.Range(1.5f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //穴のサイズを徐々に大きくする
        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x + Time.deltaTime / 3.0f, 1.0f, _maxSize),
                                                                 Mathf.Clamp(transform.localScale.y + Time.deltaTime / 3.0f, 1.0f, _maxSize));

        if(transform.localScale.x >= _maxSize && transform.localScale.y >= _maxSize)
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        _enemy.ReStartEnemy();
    }
}
