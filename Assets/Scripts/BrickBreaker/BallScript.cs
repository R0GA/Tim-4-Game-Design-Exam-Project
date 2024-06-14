using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D myRb;
    [SerializeField]
    private float speed;
    private int score = 0;
    private Vector2 startPosition;
    private int lives = 1;
    private bool ballMoving = false;
    PlayerManager playerManager = PlayerManager.Instance;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            score++;
            Debug.Log("Current Score: " + score);
        }
        else if (collision.gameObject.CompareTag("BottomWall"))
        {
            transform.position = startPosition;
            myRb.velocity = Vector2.zero;
            ballMoving = false;
            lives--;
            Debug.Log("Lives Left: " + lives);
        }
    }

  
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) && !ballMoving && lives > 0)
        {
            SetRandomTrajectory();
        }

        if (playerManager.InBossMinigame)
        {
            if(score == 15)
            {
                SceneManager.LoadScene("GameScene_DK");
            }
            else if(lives == 0)
            {
                playerManager.InBossMinigame = false;
                playerManager.ExitMiniGame(0);
            }
        }
        else
        {
            if (score == 40 || lives == 0)
            {
                if(playerManager.currentPlayer == 1)
                {
                    playerManager.ExitMiniGame(Mathf.RoundToInt(score * playerManager.p1ScoreMultiplier));
                }
                else if (playerManager.currentPlayer == 2)
                {
                    playerManager.ExitMiniGame(Mathf.RoundToInt(score * playerManager.p2ScoreMultiplier));
                }
            }
        }
    }

    private void SetRandomTrajectory()
    {
        ballMoving = true;
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-0.5f, 0.5f);
        force.y = -1;

        myRb.velocity = (force.normalized * speed);
    }

    

}
