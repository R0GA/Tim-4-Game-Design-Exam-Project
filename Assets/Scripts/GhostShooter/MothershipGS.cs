using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipGS : MonoBehaviour
{
    public int scoreValue;
    private const float maxLeft = -11f;
    private float speed = 5;
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if (transform.position.x <= maxLeft)
        {
            Destroy(gameObject);
        }
    }
}
