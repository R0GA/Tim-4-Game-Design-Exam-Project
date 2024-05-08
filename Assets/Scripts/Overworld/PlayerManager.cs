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
        SceneManager.sceneLoaded += OnSceneLoaded;

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
        if (player != null)
        {

            player.GetComponent<PlayerController>().SetActivePlayer(true);
            if (player == player1)
            {

                player2.GetComponent<PlayerController>().SetActivePlayer(false);
            }
            else
            {

                

                player1.GetComponent<PlayerController>().SetActivePlayer(false);
            }
        }
    }

    public void EnterMiniGame(House house)
    {
        if (house.MiniGame != MiniGame.Nothing)
        {
            switch (house.MiniGame)
            {
                case MiniGame.Brickbreaker:
                    SceneManager.LoadScene("GhostBreaker");
                    break;
                case MiniGame.FlappyBird:
                    SceneManager.LoadScene("Flappy Bird");
                    break;
                case MiniGame.SpaceInvaders:
                    break;
                case MiniGame.Snake:
                    SceneManager.LoadScene("Snake");
                    break;
            }
        }
        else
        {
            int ran = Random.Range(0, 100);
            if (ran % 2 == 0)
            {
                SceneManager.LoadScene("Flappy Bird");
            }
            else
            {
                SceneManager.LoadScene("GhostBreaker");
            }
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

       
        SceneManager.LoadScene("MainScene");
    }
}
