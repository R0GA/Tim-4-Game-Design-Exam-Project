using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
   // public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    GameObject playerObject;
    [SerializeField] private Spawner spawner;
    GameObject spawnerObject;
    [SerializeField] private Text scoreText;
    GameObject scoreTextObject;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    PlayerManager playerManager = PlayerManager.Instance;

    private int score;
    public int Score => score;

    /* private void Awake()
      {
          if (Instance != null)
          {
              DestroyImmediate(gameObject);
          }
          else
          {
              Instance = this;
              Application.targetFrameRate = 60;
              DontDestroyOnLoad(gameObject);
              Pause();

              playButton.SetActive(true);
              gameOver.SetActive(false);
          }
      }

      private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
      {
          Pause();

         playButton.SetActive(true);
         gameOver.SetActive(false);
      }*/

    private void Start()
    {

            playerObject = GameObject.FindGameObjectWithTag("Player");
            player = playerObject.GetComponent<Player>();
            spawnerObject = GameObject.FindGameObjectWithTag("Spawner");
            spawner = spawnerObject.GetComponent<Spawner>();
            scoreTextObject = GameObject.FindGameObjectWithTag("ScoreText");
            scoreText = scoreTextObject.GetComponent<Text>();
            playButton = GameObject.FindGameObjectWithTag("PlayButton");
            gameOver = GameObject.FindGameObjectWithTag("GameOver");

            Application.targetFrameRate = 60;
           
            Pause();

            playButton.SetActive(true);
            gameOver.SetActive(false);
        
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {

        playButton.SetActive(true);
        gameOver.SetActive(true);
       // Pause();
        playerManager.ExitMiniGame(score);

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

}