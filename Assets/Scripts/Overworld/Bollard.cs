using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bollard : MonoBehaviour
{
    PlayerManager playerManager = PlayerManager.Instance;
    

    private void Start()
    {
        playerManager = PlayerManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerManager.OpenBuyUI(gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerManager.CloseBuyUI();
    }


}
