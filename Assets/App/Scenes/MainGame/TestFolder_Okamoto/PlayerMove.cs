using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Camera cam;
    public Transform Marker;
    private float distance_two;
    public float speed = 1.0F;

    // for Game State
    GameObject objGameManager;

    // Start is called before the first frame update
    void Start()
    {
        objGameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {

        // judge Game State
        bool isGame = true;
        GameStateContoller.GameStatus status;
        status = objGameManager.GetComponent<GameStateContoller>().GetGameStatus();
        if(status == GameStateContoller.GameStatus.Ready
            || status == GameStateContoller.GameStatus.ReadyReverse)
        {
            isGame = false;
        }

        // ClikDown
        if (Input.GetMouseButtonDown(0)
            && isGame)

        {
            transform.LookAt(Marker);
            Vector3 Pinpos = gameObject.transform.position;
            distance_two = Vector3.Distance(Pinpos, Marker.position);
            float present_Location = (Time.time * speed) / distance_two;
            transform.position = Vector3.MoveTowards(Pinpos, Marker.position, present_Location);
        }
    }
}
