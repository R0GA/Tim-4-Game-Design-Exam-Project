using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Meghan Rogers Ghost shooter Minigame
public class GhostMaster : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject mothershipPrefab;

    //how far the ghosts move horizontally and vertically
    private Vector3 hMoveDistance = new Vector3(0.1f,0 ,0); //0,05, 0, 0
    private Vector3 vMoveDistance = new Vector3(0, 0.3f,0); //0, 0.15, 0
    private Vector3 mothershipSpawnPos = new Vector3(5f, 4f, 0);
    
    //nts: check PlayerGS if I'm confused. It's self explanatory, Meghan Jane. Use my brain.
    private const float MaxLeft = -9.4f;
    private const float MaxRight = 8.3f;

    private const float maxMoveSpeed = 0.0002f; //0.02
    
    //creating an enemy speed up code. Less enemies = faster movements. duh.
    private float moveTimer = 0.01f;
    private const float moveTime = 0.0005f; //0.005
    
    private float shootTimer = 3f;
    private const float shootTime = 3f;

    private float mothershipTimer = 15f;
    private const float mothershipMinTime = 45f;
    private const float mothershipMaxTime = 15f;
    
    
    //moving left/right
    private bool movingRight;

    //the block of Ghosts. Sensing all enemies. 
    public static List<GameObject> allGhosts = new List<GameObject>();
    
    void Start()
    {
        //sorry, Tim, for I have sinned.
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Ghost")) //i can hear him screaming at me
        {
            allGhosts.Add(go);
        }
    }
    void Update()
    {
        //setting the game to speed up 
        if(moveTimer <= 0)
            MoveGhosts();

        if (shootTimer <= 0 )
            Shoot();
        
        if (mothershipTimer <= 0 )
            SpawnMothership();

        moveTimer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;
        mothershipTimer -= Time.deltaTime;
    }

    private void MoveGhosts()
    {
        //setting all the ghosts to move in unison 
        if (allGhosts.Count > 0)
        {
            int hitMax = 0;
            //hitting barriers and changing direction
            for (int i = 0; i < allGhosts.Count; i++)
            {
                
                if (movingRight)
                    allGhosts[i].transform.position += hMoveDistance;
                else
                    allGhosts[i].transform.position -= hMoveDistance;

                if (allGhosts[i].transform.position.x > MaxRight || allGhosts[i].transform.position.x < MaxLeft)
                hitMax++;
            }

            if (hitMax > 0)
            {
                for (int i = 0; i < allGhosts.Count; i++)
                    allGhosts[i].transform.position -= vMoveDistance;

                movingRight = !movingRight;
            }

            moveTimer = GetMoveSpeed();
        }
    }

    private void Shoot()
    {
        Vector2 pos = allGhosts[Random.Range(0, allGhosts.Count)].transform.position;

        Instantiate(bulletPrefab, pos, Quaternion.identity);

        shootTimer = shootTime;
    }

    private void SpawnMothership()
    {
        Instantiate(mothershipPrefab, mothershipSpawnPos, Quaternion.identity);
        mothershipTimer = Random.Range(mothershipMinTime, mothershipMaxTime);
    }

    private float GetMoveSpeed()
    {
        //letting my code do all the maths
        float f = allGhosts.Count * moveTime;
        
        if(f < maxMoveSpeed)
            return maxMoveSpeed;
        else 
            return f;
    }
}
