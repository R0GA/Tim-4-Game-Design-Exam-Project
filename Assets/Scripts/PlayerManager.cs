using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public int player1Score = 0;
    public int player2Score = 0;

    private int currentPlayer = 1; // Player 1 starts
    private GameObject player1;
    private GameObject player2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event

        // Find and assign player objects when the scene is loaded
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        if (currentPlayer == 1)
        {
            SetActivePlayer(player1);
        }
        else
        {
            SetActivePlayer(player2);
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find and assign player objects when the scene is loaded
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        if (currentPlayer == 1)
        {
            SetActivePlayer(player1);
        }
        else
        {
            SetActivePlayer(player2);
        }
    }

    public void SetActivePlayer(GameObject player)
    {

        Debug.Log("Called");

        if (player != null)
        {

            Debug.Log("Called1");

            player.GetComponent<PlayerController>().SetActivePlayer(true);
            if (player == player1)
            {

                Debug.Log("Called2");

                player2.GetComponent<PlayerController>().SetActivePlayer(false);
            }
            else
            {

                Debug.Log("Called3");

                player1.GetComponent<PlayerController>().SetActivePlayer(false);
            }
        }
    }

    public void EnterMiniGame()
    {

        int ran = Random.Range(0, 100);

        Debug.Log(ran);

        if(ran%2 == 0)
        {
            SceneManager.LoadScene("Flappy Bird");
        }
        else
        {
            SceneManager.LoadScene("GhostBreaker");
        }
    }

    public void ExitMiniGame(int scoreGained)
    {
        if (currentPlayer == 1)
        {
            player1Score += scoreGained;
        }
        else
        {
            player2Score += scoreGained;
        }

        // Switch player turn
        currentPlayer = currentPlayer == 1 ? 2 : 1;

        if (currentPlayer == 1)
        {
            SetActivePlayer(player1);
        }
        else
        {
            SetActivePlayer(player2);
        }

        // You can add code here to handle scene transition back to the main scene
        SceneManager.LoadScene("MainScene");
    }
}
