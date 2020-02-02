using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    [SerializeField] private GameObject _modelMove;
    [SerializeField] private GameObject _modelPause;

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
            //transform.LookAt(Marker);
            float ry = 90.0f;
            if(Marker.position.x < transform.position.x)
            {
                ry = -90.0f;
            }
            transform.DORotate(new Vector3(0.0f, ry, 0.0f), 0.5f);
            ChangeModel(true);
            MarkerPos._MarkerPos = Marker.position;
            var obj = Instantiate(Light, Marker.position, Quaternion.identity);
            Destroy(obj.gameObject, 1.0f);
        }

        if (Pin == true)
        {
            var PMarker = MarkerPos._MarkerPos;
            var PlayerPos = transform.position;
            distance_two = Vector3.Distance(PlayerPos, PMarker);
            transform.position = Vector3.SmoothDamp(PlayerPos, PMarker, ref velocity, smoothTime);
            if ((transform.position - PMarker).sqrMagnitude < 0.1f)
            {
                Pin = false;
                ChangeModel(false);
            }
        }
    }

    private void ChangeModel(bool isMove)
    {
        _modelMove.gameObject.SetActive(isMove);
        _modelPause.gameObject.SetActive(isMove == false);
    }
 }

        
    

