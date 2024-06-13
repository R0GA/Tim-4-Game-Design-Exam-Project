using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBulletGS : MonoBehaviour
{

    private float speed = 7;
    void Update()
    {
        //bullet of player being shot up at the ghosties
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //for collision of the bullet with the ghost bois
    }
    
}
