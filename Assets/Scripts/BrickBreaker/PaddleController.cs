using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class PaddleController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 5f;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement direction
        movement = new Vector2(horizontalInput, 0f) * moveSpeed * Time.deltaTime;

        // Move the object
        transform.Translate(movement);

    }

    void FixedUpdate()
    {

     

    }

}
