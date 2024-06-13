using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
//Meghan Rogers Ghost Shooter

//nts: control, select and drag into scene creates looping animation!!! HOW DID I NOT KNOW THAT?!?!?
public class PlayerGS : MonoBehaviour
{
    public PlayerStats playerStats; 
    public GameObject bulletPrefab;

    private Vector2 offScreenPos = new Vector2(0, -20f);
    private Vector2 startingPos = new Vector2(0, -4.25f);

    private const float MaxLeft = -8.15f;
    private const float MaxRight = 8.3f;

    private float speed = 3;
    private float cooldown = 0.5f;

    private bool isShooting;

    private void Start()
    {
        playerStats.currentHealth = playerStats.maxHealth;
        playerStats.currentLives = playerStats.maxLives;

        transform.position = startingPos;
        
    }

    void Update()
    {
        //the stompie horizontal movement of the wittle guy
        if (Input.GetKey(KeyCode.A) && transform.position.x > MaxLeft || Input.GetKey(KeyCode.LeftArrow) && transform.position.x > MaxLeft)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < MaxRight || Input.GetKey(KeyCode.RightArrow) && transform.position.x < MaxRight)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed );
        }
        //shooting and cooldown of the pewpews
        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
        
    }

    private void TakeDamage()
    {
        playerStats.currentHealth--;
        if (playerStats.currentHealth <= 0)
        {
            playerStats.currentLives--;

            if (playerStats.currentLives <= 0)
            {
                Debug.Log("GAME OVER");
                Time.timeScale = 0;

            }
            else
            {
                StartCoroutine(Respawn());
            }
        }
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(cooldown);

        isShooting = false;
    }

    private IEnumerator Respawn()
    {
        Time.timeScale = 0;
        transform.position = offScreenPos;
        
        yield return new WaitForSeconds(10);
        playerStats.currentHealth = playerStats.maxHealth;

        transform.position = startingPos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GhostBullet"))
        {
            Debug.Log("player was hit!");
            TakeDamage();
            Destroy(collision.gameObject);
            Time.timeScale = 0;

        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GhostBullet"))
        {
            StartCoroutine(Respawn());
        }
        if (collision.gameObject.CompareTag("Ghost"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject); 
            Time.timeScale = 0;
            Debug.Log("GAME OVER");
           
        }
    }
}
