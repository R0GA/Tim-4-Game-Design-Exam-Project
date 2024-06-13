
using UnityEngine;


[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(Movement_PM))]
public class Ghost_PM : MonoBehaviour
{
    public Movement_PM movement { get; private set; }
    public GhostHome_PM home { get; private set; }
    public GhostScatter_PM scatter { get; private set; }
    public GhostChase_PM chase { get; private set; }
    public GhostFrightened_PM frightened { get; private set; }
    public GhostBehavior_PM initialBehavior;
    public Transform target;
    public int points = 200;
    GameManager_PM gameManager;

    private void Awake()
    {
        movement = GetComponent<Movement_PM>();
        home = GetComponent<GhostHome_PM>();
        scatter = GetComponent<GhostScatter_PM>();
        chase = GetComponent<GhostChase_PM>();
        frightened = GetComponent<GhostFrightened_PM>();
    }

    private void Start()
    {
        ResetState();
        gameManager = GameObject.FindGameObjectWithTag("GameManager_PM").GetComponent<GameManager_PM>();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();

        frightened.Disable();
        chase.Disable();
        scatter.Enable();

        if (home != initialBehavior)
        {
            home.Disable();
        }

        if (initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (frightened.enabled)
            {
                gameManager.GhostEaten(this);
            }
            else
            {
                gameManager.PacmanEaten();
            }
        }
    }

}
