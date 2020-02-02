using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressControl : MonoBehaviour
{
    GameObject objDress;
    bool isRotated;
    [SerializeField, Header("回転する時間")]
    float counterRotateMax = 2.0f;
    float counter;

    // Game Manager
    GameObject objGameManager;

    // Start is called before the first frame update
    void Start()
    {
        objDress = GameObject.Find("dress_NN_v2");
        isRotated = false;
        counter = 0.0f;

        // Game Manager
        objGameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        GameStateContoller.GameStatus status = objGameManager.GetComponent<GameStateContoller>().GetGameStatus();
        if(status == GameStateContoller.GameStatus.ReadyReverse)
        {
            if(isRotated == false)
            {
                if(counter < counterRotateMax)
                {
                    float angleY = 180.0f - 180.0f * ( counter / counterRotateMax );

                    objDress.transform.rotation = Quaternion.AngleAxis(angleY, new Vector3(0, 1, 0));

                    // counter
                    counter += Time.deltaTime;
                }
                else
                {
                    isRotated = true;
                }
            }
            if(isRotated)
            {
                objDress.transform.rotation = Quaternion.AngleAxis(0.0f, new Vector3(0, 1, 0));
            }
        }
    }
}
