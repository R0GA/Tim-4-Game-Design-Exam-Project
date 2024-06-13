using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_DK : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] runSprites;
    public Sprite climbSprite;
    private int spriteIndex;

    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    private Collider2D[] overlaps = new Collider2D[4];
    private Vector2 direction;

    private bool grounded;
    private bool climbing;

    public float moveSpeed = 3f;
    public float jumpStrength = 4f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(AnimateSprite), 1f/12f, 1f/12f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        CheckCollision();
        SetDirection();
    }

    private void CheckCollision()
    {
        grounded = false;
        climbing = false;

        
        float skinWidth = 0.1f;

        Vector2 size = collider.bounds.size;
        size.y += skinWidth;
        size.x /= 2f;

        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, overlaps);

        for (int i = 0; i < amount; i++)
        {
            GameObject hit = overlaps[i].gameObject;

            if (hit.layer == LayerMask.NameToLayer("Ground"))
            {
                
                grounded = hit.transform.position.y < (transform.position.y - 0.5f + skinWidth);
                Physics2D.IgnoreCollision(overlaps[i], collider, !grounded);
            }
            else if (hit.layer == LayerMask.NameToLayer("Ladder"))
            {
                climbing = true;
            }
        }
    }

    private void SetDirection()
    {
        if (climbing) 
        {
            direction.y = Input.GetAxis("Vertical") * moveSpeed;
        } else if (grounded && Input.GetButtonDown("Jump")) {
            direction = Vector2.up * jumpStrength;
        } else {
            direction += Physics2D.gravity * Time.deltaTime;
        }

        direction.x = Input.GetAxis("Horizontal") * moveSpeed;

        

        if (grounded) 
        {
            direction.y = Mathf.Max(direction.y, -1f);
        }

        if (direction.x > 0f) {
            transform.eulerAngles = Vector3.zero;
        } else if (direction.x < 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime);
    }

    private void AnimateSprite()
    {
        if (climbing)
        {
            spriteRenderer.sprite = climbSprite;
        }
        else if (direction.x != 0f)
        {
            spriteIndex++;

            if (spriteIndex >= runSprites.Length) {
                spriteIndex = 0;
            }

            if (spriteIndex > 0 && spriteIndex <= runSprites.Length) {
                spriteRenderer.sprite = runSprites[spriteIndex];
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Objective"))
        {
            FindObjectOfType<GameManager_DK>().LevelComplete();
            enabled = false;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
           
            RestartGame();
        }
    }
    private void RestartGame()
    {
        //SceneManager.LoadScene("StartScene_DK");

        int ran = 3; //Random.Range(1, 6);
        Debug.Log(ran);
        if (ran == 1)
        {
            SceneManager.LoadScene("GhostBreaker");
        }
        else if (ran == 2)
        {
            SceneManager.LoadScene("Flappy Bird");
        }
        else if (ran == 3)
        {
            SceneManager.LoadScene("GhostShooterMiniGame");
        }
        else if (ran == 4)
        {
            SceneManager.LoadScene("GhostCollector");
        }
        else if (ran == 5)
        {
            SceneManager.LoadScene("Pacman");
        }

    }

}
