using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager_PM : MonoBehaviour
{
    //public static GameManager_PM Instance { get; private set; }

    [SerializeField] private Ghost_PM[] ghosts;
    [SerializeField] private Pacman_PM pacman;
    [SerializeField] private Transform pellets;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private int ghostMultiplier = 1;
    private int lives = 1;
    private int score = 0;

    public PlayerManager playerManager = PlayerManager.Instance;

    public int Lives => lives;
    public int Score => score;

    private void Awake()
    {
        /*
         if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
      */
    }

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }

        if (playerManager.InBossMinigame)
        {
            if (score >= 2000)
            {
                SceneManager.LoadScene("GameScene_DK");
            }
        }

    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(1);
        NewRound();
    }

    private void NewRound()
    {
        gameOverText.enabled = false;

        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        pacman.ResetState();

        /*for (int i = 0; i < ghosts.Length; i++)
        { if (ghosts[i] != null)
            {
                Debug.LogError($"Ghost_PM at index {i} is null");
            }
            else
            {
                ghosts[i].ResetState();
            }
        }
        if (pacman == null)
        {
            Debug.LogError("Pacman_PM is null");
        }
        else
        { pacman.ResetState(); }*/
    }

    private void GameOver()
    {
        if (playerManager.InBossMinigame)
        {
            playerManager.ExitMiniGame(0);
            playerManager.InBossMinigame = false;
        }
        else
            playerManager.ExitMiniGame(score / 100);

       /* gameOverText.enabled = true;

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
       */
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void PacmanEaten()
    {
        pacman.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0)
        {
            Invoke(nameof(ResetState), 3f);
        }
        else
        {
            GameOver();
        }
    }

    public void GhostEaten(Ghost_PM ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);

        ghostMultiplier++;
    }

    public void PelletEaten(Pellet_PM pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletEaten(PowerPellet_PM pellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }

}