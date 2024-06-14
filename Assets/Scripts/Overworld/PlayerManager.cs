using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public int player1Score = 0;
    public int player2Score = 0;
    public int currentPlayer = 1; // Player 1 starts
    public GameObject buyCanvas;
    public float p1ScoreMultiplier;
    public float p2ScoreMultiplier;

    private GameObject player1;
    private GameObject player2;
    [SerializeField]
    private TMP_Text costTXT;
    private TMP_Text b1Text;
    private TMP_Text b2Text;
    private int bolCost;
    private GameObject gameCanvas;
    private GameObject infoPanel;
    private GameObject scorePanel;
    private GameObject firstPlayPanel;
    private bool firstPlay = true;
 
    

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
        gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
        scorePanel = GameObject.FindGameObjectWithTag("ScorePanel");
        infoPanel = GameObject.FindGameObjectWithTag("InfoPanel");
        firstPlayPanel = GameObject.FindGameObjectWithTag("FPP");

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
            costTXT = GameObject.FindGameObjectWithTag("BuyTXT").GetComponent<TMP_Text>();
        if (buyCanvas != null)
            b1Text = GameObject.FindGameObjectWithTag("B1TXT").GetComponent<TMP_Text>();
        if (buyCanvas != null)
            b2Text = GameObject.FindGameObjectWithTag("B2TXT").GetComponent<TMP_Text>();
        if(infoPanel != null)
            infoPanel.gameObject.SetActive(false);
       

        if (buyCanvas != null)
            b1Text.gameObject.SetActive(false);
        if (buyCanvas != null)
            b2Text.gameObject.SetActive(false);
        if (buyCanvas != null)
            buyCanvas.gameObject.SetActive(false);

        if (firstPlay)
        {
            firstPlayPanel.gameObject.SetActive(true);
        }
        else
        {
            if (firstPlayPanel != null)
            firstPlayPanel.gameObject.SetActive(false);
        }

        

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
        gameCanvas = GameObject.FindGameObjectWithTag("GameCanvas");
        scorePanel = GameObject.FindGameObjectWithTag("ScorePanel");
        infoPanel = GameObject.FindGameObjectWithTag("InfoPanel");
        firstPlayPanel = GameObject.FindGameObjectWithTag("FPP");

        if (bollard1 != null)
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
        if (bollard1 != null)

        if (buyCanvas != null)
            costTXT = GameObject.FindGameObjectWithTag("BuyTXT").GetComponent<TMP_Text>();
        if (buyCanvas != null)
            b1Text = GameObject.FindGameObjectWithTag("B1TXT").GetComponent<TMP_Text>();
        if (buyCanvas != null)
            b2Text = GameObject.FindGameObjectWithTag("B2TXT").GetComponent<TMP_Text>();

        if (buyCanvas != null)
            b1Text.gameObject.SetActive(false);
        if (buyCanvas != null)
            b2Text.gameObject.SetActive(false);
        if (buyCanvas != null)
            buyCanvas.gameObject.SetActive(false);
        if (infoPanel != null)
            infoPanel.gameObject.SetActive(false);

        if (firstPlay)
        {
            firstPlayPanel.gameObject.SetActive(true);
        }
        else
        {
            if (firstPlayPanel != null)
            firstPlayPanel.gameObject.SetActive(false);
        }

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
                    SceneManager.LoadScene("GhostShooterMiniGame");
                    break;
                case MiniGame.Snake:
                    SceneManager.LoadScene("GhostCollector");
                    break;
                case MiniGame.PacMan:
                    SceneManager.LoadScene("Pacman");
                    break;
                case MiniGame.Random:
                    int ran = Random.Range(1, 6);
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

            if(player1Score >= bolCost)
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
                
                player1Score = player1Score - bolCost;
                //Destroy(currentBollard);
                buyCanvas.gameObject.SetActive(false);
            }

        }
        else if (currentPlayer == 2)
        {

            if (player2Score >= bolCost)
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
                player2Score = player2Score - bolCost;
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

    public void OpenBuyUI(GameObject bol, int cost)
    {
        buyCanvas.gameObject.SetActive(true);
        currentBollard = bol;
        costTXT.text = "Would you like to open the next section for " + cost.ToString() + " points?";
        bolCost = cost;

        Debug.Log(currentBollard);
        Debug.Log(b2Text);

        if (currentBollard == bollard1)
        {
            b1Text.gameObject.SetActive(true);
        }
        else if (currentBollard == bollard2)
        {
            Debug.Log("yep");
            b2Text.gameObject.SetActive(true);
        }
    }
    public void CloseBuyUI()
    {
        b1Text.gameObject.SetActive(false);
        b2Text.gameObject.SetActive(false);
        buyCanvas.gameObject.SetActive(false);
    }

    public void OpenInfoPanel()
    {
        scorePanel.gameObject.SetActive(false);
        infoPanel.gameObject.SetActive(true);
    }

    public void CloseInfoPanel()
    {
        scorePanel.gameObject.SetActive(true);
        infoPanel.gameObject.SetActive(false);
    }

    public void CloseFPP()
    {
        firstPlayPanel.gameObject.SetActive(false);
        firstPlay = false;
    }

}
