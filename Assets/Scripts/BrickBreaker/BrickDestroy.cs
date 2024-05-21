using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDestroy : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
        }
    }

}