using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostGS : MonoBehaviour
{
    public int scoreValue;
    public GameObject explosion;
   

    public void Kill()
    {
        UIManagerGS.UpdateScore(scoreValue);
        GhostMaster.allGhosts.Remove(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Time.timeScale = 0;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
