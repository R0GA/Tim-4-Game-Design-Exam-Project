using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Meghan Rogers Ghost Shooter Minigame
public class PlayerBulletGS : MonoBehaviour
{

    private float speed = 10;
    void Start()
    {
        
    }

   
    void Update()
    {
        
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            collision.gameObject.GetComponent<GhostGS>().Kill();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("GhostBullet"))
        {
           Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
