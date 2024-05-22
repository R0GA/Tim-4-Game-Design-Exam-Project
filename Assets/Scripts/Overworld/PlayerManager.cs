using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public int player1Score = 0;
    public int player2Score = 0;
    public int currentPlayer = 1; // Player 1 starts
    public GameObject buyCanvas;

    private GameObject player1;
    private GameObject player2;
    private GameObject currentBollard;
    [SerializeField]
    private GameObject bollard1;
    [SerializeField] 
    private GameObject bollard2;
    [SerializeField]
    private GameObject bollard3;
    private bool b1Bought = false;
    private bool b2Bought = false;
    private bool b3Bought = false;

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
        bollard1 = GameObject.FindGameObjectWithTag("Bollard1");
        bollard2 = GameObject.FindGameObjectWithTag("Bollard2");
        bollard3 = GameObject.FindGameObjectWithTag("Bollard3");
        buyCanvas = GameObject.FindGameObjectWithTag("BuyCanvas");
        buyCanvas.gameObject.SetActive(false);

        if (currentPlayer == 1)
        {
            SetActivePlayer(player1);
        }
        else
        {
            SetActivePlayer(player2);
        }

        if (b1Bought)
        {
            Destroy(bollard1);
        }
        else if (b2Bought)
        {
            Destroy(bollard2);
        }
        else if (b3Bought)
        {
            Destroy(bollard3);
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find and assign player objects when the scene is loaded
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        bollard1 = GameObject.FindGameObjectWithTag("Bollard1");
        bollard2 = GameObject.FindGameObjectWithTag("Bollard2");
        bollard3 = GameObject.FindGameObjectWithTag("Bollard3");
        buyCanvas = GameObject.FindGameObjectWithTag("BuyCanvas");

        if (buyCanvas != null)
        buyCanvas.gameObject.SetActive(false);


        if (currentPlayer == 1)
        {
            SetActivePlayer(player1);
        }
        else
        {
            SetActivePlayer(player2);
        }

        if (b1Bought)
        {
            Destroy(bollard1);
        }
        else if (b2Bought)
        {
            Destroy(bollard2);
        }
        else if (b3Bought)
        {
            Destroy(bollard3);
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
                currentPlayer = 1;
            }
            else
            {
                player1.GetComponent<PlayerController>().SetActivePlayer(false);
                currentPlayer = 2;
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
                case MiniGame.Random:
                    int ran = Random.Range(1, 5);
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
                        Debug.Log("Space Invaders");
                    }
                    else if (ran == 4)
                    {
                        SceneManager.LoadScene("Snake");
                    }
                    break;
            }
        }
        /*else
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
        }*/
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

    public void TryBuy()
    {
        if(currentPlayer == 1)
        {

            if(player1Score >= 20)
            {
                if(currentBollard == bollard1)
                {
                    b1Bought = true;
                }
                else if(currentBollard == bollard2)
                {
                    b2Bought = true;
                }
                else if(currentBollard == bollard3)
                {
                    b3Bought = true;
                }
                
                player1Score = player1Score - 20;
                Destroy(currentBollard);
                buyCanvas.gameObject.SetActive(false);
            }

        }
        else if (currentPlayer == 2)
        {

            if (player2Score >= 20)
            {
                if (currentBollard == bollard1)
                {
                    b1Bought = true;
                }
                else if (currentBollard == bollard2)
                {
                    b2Bought = true;
                }
                else if (currentBollard == bollard3)
                {
                    b3Bought = true;
                }
                player2Score = player2Score - 20;
                Destroy(currentBollard);
                buyCanvas.gameObject.SetActive(false);
            }

        }

    }

    public void OpenBuyUI(GameObject bol)
    {
        buyCanvas.gameObject.SetActive(true);
        currentBollard = bol;
    }
    public void CloseBuyUI()
    {
        buyCanvas.gameObject.SetActive(false);
    }

}
