using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pellet_PM : MonoBehaviour
{
    public int points = 10;
    GameManager_PM gameManager;

    private void Start()
    {
      gameManager = GameObject.FindGameObjectWithTag("GameManager_PM").GetComponent<GameManager_PM>();
    }

    protected virtual void Eat()
    {
       gameManager.PelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }

}

