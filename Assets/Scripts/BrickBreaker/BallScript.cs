using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D myRb;
    [SerializeField]
    private float speed;
    private int score = 0;
    private Vector2 startPosition;
    private int lives = 3;
    private bool ballMoving = false;
    PlayerManager playerManager = PlayerManager.Instance;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var paddleController = collision.gameObject.GetComponent<PaddleController>();
        
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
        else if (paddleController != null)
        {
            var velocity = myRb.velocity.normalized;
            var returnVelocity = -Vector2.Reflect(velocity, collision.GetContact(0).normal);
            returnVelocity.x *= -1;
            myRb.velocity = returnVelocity * speed;
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

        if(score == 40 || lives == 0)
        {
            playerManager.ExitMiniGame(score);
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
