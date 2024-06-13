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

    [SerializeField]
    private UIManagerGS uiManager;
    [SerializeField]
    private GhostMaster ghostMaster;

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

    private IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(cooldown);

        isShooting = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ghost") || collision.CompareTag("GhostBullet"))
        ghostMaster.EndGame(uiManager.score);
    }
}
