using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust as needed
    private Rigidbody2D rb;
    private bool isActivePlayer = false; // Track if this player is active

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

  

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Spot1");

        if (other.CompareTag("House")) // Change key as needed
        {
            Debug.Log("Spot2");

            if (Input.GetKey(KeyCode.Space))
            {
                PlayerManager.Instance.EnterMiniGame(); // Call method to switch to minigame
            }
        }
    }

    private void Update()
    {
       /* if (isActivePlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Change key as needed
            {
                InteractWithHouse();
            }
        }*/
    }

    private void FixedUpdate()
    {
        if (isActivePlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb.velocity = movement * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop movement if not active player
        }
    }

    /*private void InteractWithHouse()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f); // Adjust radius as needed
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("House"))
            {
                if (isActivePlayer)
                {
                    PlayerManager.Instance.EnterMiniGame(); // Call method to switch to minigame
                }
                break;
            }
        }
    }*/

    public void SetActivePlayer(bool active)
    {
        isActivePlayer = active;
    }
}