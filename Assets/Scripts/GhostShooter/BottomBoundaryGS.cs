using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BottomBoundaryGS : MonoBehaviour
{
    public GameObject ghosts;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.gameObject.CompareTag("Ghost"))
        {
            Time.timeScale = 0;
            Destroy(ghosts);
        }
    }
}
