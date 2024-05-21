using UnityEngine;

public class BMMovementController : MonoBehaviour
{
   public new Rigidbody2D rigidbody {  get; private set; }

    private Vector2 direction = Vector2.down;

    public float speed = 5f;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public BMAnimatedSpriteRenderer spriteRendererUp; 
    public BMAnimatedSpriteRenderer spriteRendererDown;
    public BMAnimatedSpriteRenderer spriteRendererLeft;
    public BMAnimatedSpriteRenderer spriteRendererRight;
    public BMAnimatedSpriteRenderer spriteRendererDeath;

    private BMAnimatedSpriteRenderer activeSpriterender;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriterender = spriteRendererDown;
    }

    private void Update()
    {
        if (Input.GetKey(inputUp)) {SetDirection (Vector2.up, spriteRendererUp); 
        }
        else if (Input.GetKey(inputDown)) { SetDirection(Vector2.down, spriteRendererDown); }
        else if (!Input.GetKey(inputLeft)) { SetDirection(Vector2.left, spriteRendererLeft); }
        else if (!Input.GetKey(inputRight)) { SetDirection(Vector2.right, spriteRendererRight); }
        else { SetDirection(Vector2.zero, activeSpriterender); }
    }


    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position; 
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
         
        rigidbody.MovePosition(position + translation);
    }
    private void SetDirection(Vector2 newDirection, BMAnimatedSpriteRenderer spriteRenderer)
    { 
      direction = newDirection;
        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererUp.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererUp.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererUp.enabled = spriteRenderer == spriteRendererRight;
     

        activeSpriterender = spriteRenderer;
        activeSpriterender.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer ==LayerMask.NameToLayer("Explosiob"))
        {
            DeathSequence();

        }
    }

    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BMBombController>().enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererUp.enabled=false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    
    }

    private void OnDeathSequenceEnded()
    { 
       gameObject.SetActive(false);
        FindObjectOfType<BMGameManager>().CheckWinState();
      
    }
}
