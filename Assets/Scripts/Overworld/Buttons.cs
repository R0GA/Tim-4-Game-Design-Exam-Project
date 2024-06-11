using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    PlayerManager playerManager = PlayerManager.Instance;
    private void Start()
    {
        playerManager = PlayerManager.Instance;
    }

    public void Buy()
    {
        playerManager.TryBuy();
    }
    public void Close()
    {
        playerManager.CloseBuyUI();
    }
}
