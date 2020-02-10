using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField]
    Transform ball;
  
    public static Vector3 offset1;
     Vector3 offset2;
    public static bool gameStarted;


    private void OnEnable()
    {
        ball = GameObject.Find("Ball").transform;
    }
    private void Start()
    {
       // gameStarted = false;
        offset1 = new Vector3(0f, 2f, -3f);
        //offset1 = new Vector3(0f, 3f, 6f);
    }
    private void LateUpdate()
    {
        if(gameStarted)
        transform.position =offset1+ball.position;
    }
    private void Update()
    {
    }
}
