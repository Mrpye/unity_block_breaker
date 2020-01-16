using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16;
    [SerializeField] float minX = 2f;
    [SerializeField] float maxX = 14f;
    // Start is called before the first frame update
    GameSession gamesession;
    Ball ball;
    void Start()
    {
        gamesession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
       
       // float MousePostInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        Vector2 paddlePos = new Vector2(transform.transform.position.x, transform.transform.position.y);
        paddlePos.x= Mathf.Clamp(GetXpos(), minX,maxX);
        transform.position = paddlePos;

    }
    private float GetXpos() {
        if (gamesession.IsAutoPlayEnabled()) {
            return ball.transform.position.x;
        } else {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
