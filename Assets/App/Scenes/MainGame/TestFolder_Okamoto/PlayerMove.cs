using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos
{
    Vector3 _markerPos;
     public Vector3 _MarkerPos
    {
        set {_markerPos = value;}
        get { return _markerPos; }
    }
}
public class PlayerMove : MonoBehaviour
{
    Camera cam;
    public Transform Marker;
    private float distance_two;
    private float smoothTime = 0.5f;
    Vector3 velocity = Vector3.zero;
    bool Pin = false;
    Pos MarkerPos = new Pos();
    public GameObject Light;

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
        if (status == GameStateContoller.GameStatus.Ready
            || status == GameStateContoller.GameStatus.ReadyReverse)
        {
            isGame = false;
        }

        
        // ClikDown
        if (Input.GetMouseButtonDown(0)
            && isGame)

        {
            Pin = true;
            transform.LookAt(Marker);
            MarkerPos._MarkerPos = Marker.position;
            Instantiate(Light, Marker.position, Quaternion.identity);
        }

        if (Pin == true)
        {
            var PMarker = MarkerPos._MarkerPos;
            var PlayerPos = transform.position;
            distance_two = Vector3.Distance(PlayerPos, PMarker);
            transform.position = Vector3.SmoothDamp(PlayerPos, PMarker, ref velocity, smoothTime);
            if (transform.position == PMarker)
            {
                Pin = false;
            }
        }
    }
 }

        
    

