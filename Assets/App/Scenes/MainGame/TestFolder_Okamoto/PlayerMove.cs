using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform Marker;
    public GameObject _markingPinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Marker);
        
    }

   

    public void Move()
    {
        Vector3 Pinpos = _markingPinPrefab.transform.position;
        gameObject.transform.position = Pinpos;
    }
}
