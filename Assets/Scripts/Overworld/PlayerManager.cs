using System.Linq;
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

    public bool InBossMinigame =  false;

    private GameObject currentBollard;
    [SerializeField]
    private GameObject bollard1;
    [SerializeField] 
    private GameObject bollard2;
    [SerializeField]
    private GameObject bollard3;

    [SerializeField]
    private Component p1B1Coll;
    [SerializeField]
    private Component p1B2Coll;
    [SerializeField]
    private Component p1B3Coll;
    [SerializeField]
    private Component p2B1Coll;
    [SerializeField]
    private Component p2B2Coll;
    [SerializeField]
    private Component p2B3Coll;

    [SerializeField]
    private bool b1Bought = false;
    [SerializeField]
    private bool b2Bought = false;
    [SerializeField]
    private bool b3Bought = false;
    [SerializeField]
    private bool p1B1Bought = false;
    [SerializeField]
    private bool p1B2Bought = false;
    [SerializeField]
    private bool p1B3Bought = false;
    [SerializeField]
    private bool p2B1Bought = false;
    [SerializeField]
    private bool p2B2Bought = false;
    [SerializeField]
    private bool p2B3Bought = false;


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

        if (bollard1 != null)
            p1B1Coll = bollard1.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P1Collider"));
        if (bollard2 != null)
            p1B2Coll = bollard2.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P1Collider"));
        if (bollard3 != null)
            p1B3Coll = bollard3.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P1Collider"));
        if (bollard1 != null)
            p2B1Coll = bollard1.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P2Collider"));
        if (bollard2 != null)
            p2B2Coll = bollard2.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P2Collider"));
        if (bollard3 != null)
            p2B3Coll = bollard3.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P2Collider"));

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

        for (int i = 0; i < 3; i++)
        {
            if (i == 0 && p1B1Bought && p2B1Bought)
            {
                b1Bought = true;
            }
            else if (i == 2 && p1B2Bought && p2B2Bought)
            {
                b2Bought = true;
            }
            else if (i == 3 && p1B3Bought && p2B3Bought)
            {
                b3Bought = true;
            }
        }

        for (int i = 0; i < 6; i++)
        {
            if (i == 0 && p1B1Bought && bollard1 != null)
            {
                Destroy(p1B1Coll.gameObject);
            }
            else if (i == 1 && p1B2Bought && bollard2 != null)
            {
                Destroy(p1B2Coll.gameObject);
            }
            else if (i == 2 && p1B3Bought && bollard3 != null)
            {
                Destroy(p1B3Coll.gameObject);
            }
            else if (i == 3 && p2B1Bought && bollard1 != null)
            {
                Destroy(p2B1Coll.gameObject);
            }
            else if (i == 4 && p2B2Bought && bollard2 != null)
            {
                Destroy(p2B2Coll.gameObject);
            }
            else if (i == 5 && p2B3Bought && bollard3 != null)
            {
                Destroy(p2B3Coll.gameObject);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (i == 0 && b1Bought)
            {
                Destroy(bollard1);
            }
            else if (i == 1 && b2Bought)
            {
                Destroy(bollard2);
            }
            else if (i == 2 && b3Bought)
            {
                Destroy(bollard3);
            }
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

        if(bollard1 != null)
            p1B1Coll = bollard1.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P1Collider"));
        if(bollard2 != null)
            p1B2Coll = bollard2.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P1Collider"));
        if(bollard3 != null)
            p1B3Coll = bollard3.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P1Collider"));
        if (bollard1 != null)
            p2B1Coll = bollard1.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P2Collider"));
        if (bollard2 != null)
            p2B2Coll = bollard2.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P2Collider"));
        if (bollard3 != null)
            p2B3Coll = bollard3.GetComponentsInChildren<BoxCollider2D>().ToList().Find(x => x.name.Contains("P2Collider"));

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

        for (int i = 0; i < 3; i++)
        {
            if (i == 0 && p1B1Bought && p2B1Bought)
            {
                b1Bought = true;
            }
            else if (i == 2 && p1B2Bought && p2B2Bought)
            {
                b2Bought = true;
            }
            else if (i == 3 && p1B3Bought && p2B3Bought)
            {
                b3Bought = true;
            }
        }

        for (int i = 0; i < 6; i++)
        {
            if (i == 0 && p1B1Bought && bollard1 != null)
            {
                Destroy(p1B1Coll.gameObject);
            }
            else if (i == 1 && p1B2Bought && bollard2 != null)
            {
                Destroy(p1B2Coll.gameObject);
            }
            else if (i == 2 && p1B3Bought && bollard3 != null)
            {
                Destroy(p1B3Coll.gameObject);
            }
            else if (i == 3 && p2B1Bought && bollard1 != null)
            {
                Destroy(p2B1Coll.gameObject);
            }
            else if (i == 4 && p2B2Bought && bollard2 != null)
            {
                Destroy(p2B2Coll.gameObject);
            }
            else if (i == 5 && p2B3Bought && bollard3 != null)
            {
                Destroy(p2B3Coll.gameObject);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (i == 0 && b1Bought)
            {
                Destroy(bollard1);
            }
            else if (i == 1 && b2Bought)
            {
                Destroy(bollard2);
            }
            else if (i == 2 && b3Bought)
            {
                Destroy(bollard3);
            }
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
                case MiniGame.DonkeyKong:
                    InBossMinigame = true;
                    SceneManager.LoadScene("GameScene_DK");
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
                    p1B1Bought = true;
                    Destroy(p1B1Coll.gameObject);
                }
                else if(currentBollard == bollard2)
                {
                    p1B2Bought = true;
                    Destroy(p1B2Coll.gameObject);
                }
                else if(currentBollard == bollard3)
                {
                    p1B3Bought = true;
                    Destroy(p1B3Coll.gameObject);
                }
                
                player1Score = player1Score - 20;
                //Destroy(currentBollard);
                buyCanvas.gameObject.SetActive(false);
            }

        }
        else if (currentPlayer == 2)
        {

            if (player2Score >= 20)
            {
                if (currentBollard == bollard1)
                {
                    p2B1Bought = true;
                    Destroy(p2B1Coll.gameObject);
                }
                else if (currentBollard == bollard2)
                {
                    p2B2Bought = true;
                    Destroy(p2B2Coll.gameObject);
                }
                else if (currentBollard == bollard3)
                {
                    p2B3Bought = true;
                    Destroy(p2B3Coll.gameObject);
                }
                player2Score = player2Score - 20;
                //Destroy(currentBollard);
                buyCanvas.gameObject.SetActive(false);
            }

        }

        if (p1B1Bought && p2B1Bought)
            b1Bought = true;
        if (p1B2Bought && p2B2Bought)
            b2Bought = true;
        if (p1B3Bought && p2B3Bought)
            b3Bought = true;

        if (b1Bought)
            Destroy(bollard1);
        if (b2Bought)
            Destroy(bollard2);
        if (b3Bought)
            Destroy(bollard3);
        
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
