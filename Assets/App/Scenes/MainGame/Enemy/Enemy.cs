using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Ray ray; //レイ
    private RaycastHit hit; //ヒットしたオブジェクト情報
    [SerializeField] private float raydis;
    private bool rayHit;

    //Enemyの移動ポイント
    [SerializeField,Header("Enemyの移動ポイント")] private GameObject[] rootPoints;
    //Enemyの移動速度
    [SerializeField, Header("Enemyの移動速度")] private float speed;
    public int canNotGoRootNum;
    public int beforeRootNum;
    public int rootNum;

    public bool stop;

    public float randomTime;

    [SerializeField,Header("穴オブジェクト")] private GameObject hole;
    private bool instance;

    // Start is called before the first frame update
    void Start()
    {
        rayHit = true;
        rootNum = Random.Range(0, rootPoints.Length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            RootSetting(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var enemyPos = transform.position;

        ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        //Rayがhitしなかったら移動先を再設定
        if (Physics.Raycast(ray, out hit, raydis) == false)
        {
            RootSetting(2);
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raydis, Color.red);

        var timer = 0.0f;
        timer += Time.deltaTime;

        if (stop)
        {
            //Enemyの動きを停止
            transform.position = enemyPos;
            if (instance == false)
            {
                CreateHole(enemyPos);
            }
        }
        else
        {
            //rootPointに向けてEnemyを移動
            transform.position = Vector3.MoveTowards(transform.position, rootPoints[rootNum].transform.position, speed * Time.deltaTime);
            instance = false;
        }
    }

    void RootSetting(int type)
    {
        switch (type)
        {
            case 1:
                beforeRootNum = rootNum;
                rootNum = Random.Range(0, rootPoints.Length);

                switch (rayHit)
                {
                    case true:
                        //rootNumがひとつ前と同じだったら再度乱数設定
                        while (rootNum == beforeRootNum)
                        {
                            rootNum = Random.Range(0, rootPoints.Length);
                        }
                        break;

                    case false:
                        //rootNumがひとつ前と同じかつ行けないrootPointだったら再度乱数設定
                        while (rootNum == beforeRootNum || rootNum == canNotGoRootNum)
                        {
                            rootNum = Random.Range(0, rootPoints.Length);
                            rayHit = true;
                        }
                        break;
                }
                break;

            case 2:
                rayHit = false;
                canNotGoRootNum = rootNum;
                rootNum = beforeRootNum;
                break;
        }
    }

    void CreateHole(Vector3 enemyPos)
    {
        var createHole = Instantiate(hole);
        createHole.transform.position = new Vector3(enemyPos.x, enemyPos.y, enemyPos.z);
        instance = true;
    }
}
