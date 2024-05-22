using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    PlayerManager playerManager;
    [SerializeField]
    private Transform player1;
    [SerializeField] 
    private Transform player2;
    [SerializeField] 
    private CinemachineVirtualCamera vcam;

    void Start()
    {

        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();

        if(playerManager.currentPlayer == 1)
        {
            vcam.Follow = player1;
        }
        else
        {
            vcam.Follow = player2;
        }
     
    }
}
