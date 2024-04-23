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

    }

    private void SetRandomTrajectory()
    {
        ballMoving = true;
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-0.5f, 0.5f);
        force.y = -1;

        myRb.AddForce(force.normalized * speed);
    }

}
