using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BottomBoundaryGS : MonoBehaviour
{
    public GameObject ghosts;
    [SerializeField]
    GhostMaster ghostMaster;
    [SerializeField]
    UIManagerGS uiManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            ghostMaster.EndGame(uiManager.score);
        }
    }
}
