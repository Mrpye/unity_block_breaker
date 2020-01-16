using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseCollider : MonoBehaviour
{
    GameSession gamesession;
    private void OnTriggerEnter2D(Collider2D collision) {

        if (FindObjectOfType<GameSession>().LooseLife()) {
            FindObjectOfType<Ball>().ResetBall();
        } else {
            SceneManager.LoadScene("Game Over");
        }

        
    }
}
