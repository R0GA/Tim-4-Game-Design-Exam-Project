using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet_PM : Pellet_PM
{
    public float duration = 8f;
    GameManager_PM gameManagerPP;

    private void Start()
    {
        gameManagerPP = GameObject.FindGameObjectWithTag("GameManager_PM").GetComponent<GameManager_PM>();
    }
    protected override void Eat()
    {
        gameManagerPP.PowerPelletEaten(this);
    }

}
