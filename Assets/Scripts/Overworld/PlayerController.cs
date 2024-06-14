using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust as needed
    private Rigidbody2D rb;
    private bool isActivePlayer = false; // Track if this player is active

    private House currentHouse;
    private PlayerManager playerManager = PlayerManager.Instance;

    [SerializeField]
    private GameObject p1WF;
    [SerializeField]
    private GameObject p1WB;
    [SerializeField]
    private GameObject p1WL;
    [SerializeField] 
    private GameObject p1WR;
    [SerializeField]
    private GameObject p2WF;
    [SerializeField]
    private GameObject p2WB;
    [SerializeField]
    private GameObject p2WL;
    [SerializeField]
    private GameObject p2WR;


    [SerializeField]
    private KeyCode interactWithHouse = KeyCode.Space;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerManager = PlayerManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var house = collision.gameObject.GetComponent<House>();
        if (house != null)
        {
            Debug.Log($"House {house}");
            /*if (Input.GetKey(KeyCode.Space))
            {
                PlayerManager.Instance.EnterMiniGame(); // Call method to switch to minigame
            }*/
            currentHouse = house;
            
            if(playerManager.currentPlayer == 1)
            {
                playerManager.p1ScoreMultiplier = house.ScoreMultiplier;
            }
            else if(playerManager.currentPlayer == 2)
            {
                playerManager.p2ScoreMultiplier = house.ScoreMultiplier;
            }

            Debug.Log(playerManager.p1ScoreMultiplier + " " +  playerManager.p2ScoreMultiplier);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var house = collision.gameObject.GetComponent<House>();
        if (house != null)
        {
            currentHouse = null;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        /*var house = other.gameObject.GetComponent<House>();
        if (house != null)
        {
            Debug.Log($"House {house}");
            if (Input.GetKey(KeyCode.Space))
            {
                PlayerManager.Instance.EnterMiniGame(); // Call method to switch to minigame
            }
        }*/
    }

    private void Update()
    {
       if ( currentHouse != null )
       {
            if (Input.GetKey(interactWithHouse))
            {
                PlayerManager.Instance.EnterMiniGame(currentHouse);
            }
        }

       if(playerManager.currentPlayer == 1)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                p1WB.gameObject.SetActive(false);
                p1WL.gameObject.SetActive(false);
                p1WR.gameObject.SetActive(false);
                p1WF.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                p1WF.gameObject.SetActive(false);
                p1WL.gameObject.SetActive(false);
                p1WR.gameObject.SetActive(false);
                p1WB.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                p1WB.gameObject.SetActive(false);
                p1WF.gameObject.SetActive(false);
                p1WR.gameObject.SetActive(false);
                p1WL.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                p1WB.gameObject.SetActive(false);
                p1WL.gameObject.SetActive(false);
                p1WF.gameObject.SetActive(false);
                p1WR.gameObject.SetActive(true);
            }
        }
        if (playerManager.currentPlayer == 2)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                p2WB.gameObject.SetActive(false);
                p2WL.gameObject.SetActive(false);
                p2WR.gameObject.SetActive(false);
                p2WF.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                p2WF.gameObject.SetActive(false);
                p2WL.gameObject.SetActive(false);
                p2WR.gameObject.SetActive(false);
                p2WB.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                p2WB.gameObject.SetActive(false);
                p2WF.gameObject.SetActive(false);
                p2WR.gameObject.SetActive(false);
                p2WL.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                p2WB.gameObject.SetActive(false);
                p2WL.gameObject.SetActive(false);
                p2WF.gameObject.SetActive(false);
                p2WR.gameObject.SetActive(true);
            }
        }

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