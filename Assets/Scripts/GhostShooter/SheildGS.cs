using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildGS : MonoBehaviour
{
    public Sprite[] states;
    private int health;
    private SpriteRenderer sr;
    void Start()
    {
        health = 3;
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GhostBullet"))
        {
            Destroy(collision.gameObject);
            health--;

            if (health <= 0)
                Destroy(gameObject);
            else
                sr.sprite = states[health - 1];
        }

        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("Ghost"))
        {
            Destroy(collision.gameObject);
            health--;

            if (health <= 0)
                Destroy(gameObject);
            else
                sr.sprite = states[health - 3];
        }
    }
}
