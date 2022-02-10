using UnityEngine;

using Tonhex;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; } = null;

    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    [SerializeField]
    private MainUIController mainUIController;

    public LayerMask mazeLayer;
    public LayerMask playerLayer;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        NewGame();
    }

    void Update()
    {
        if (lives <= 0 && Input.anyKey) {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        // TODO: mainUIController.gameOverText.enabled = false;
        Debug.Log("GameManager::NewRound()");

        foreach (Transform pellet in pellets) {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
    }

    private void GameOver()
    {
        // TODO: mainUIController.gameOverText.enabled = true;
        Debug.Log("GAME OVER");

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        // TODO: mainUIController.lifeText.text = "x" + lives.ToString();
        Debug.Log("x" + lives.ToString());
    }

    private void SetScore(int score)
    {
        this.score = score;
        // TODO: mainUIController.scoreText.text = score.ToString().PadLeft(2, '0');
        Debug.Log(score.ToString().PadLeft(2, '0'));
    }

    public void PacmanDeath()
    {
        AudioManager.Instance.PlayEffect(AudioManager.Instance.PlayerDeathEffectAudioClip);

        pacman.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0) {
            Invoke(nameof(ResetState), 3f);
        } else {
            GameOver();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        AudioManager.Instance.PlayEffect(AudioManager.Instance.EatGhostEffectAudioClip);

        int ghostScorePoints = ghost.ScorePoints * ghostMultiplier;
        SetScore(score + ghostScorePoints);

        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet, bool playAudio = true)
    {
        if (playAudio) {
            AudioManager.Instance.PlayEffectSequence(AudioManager.Instance.PalletEffectAudioSequence);
        }

        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.ScorePoints);

        if (!HasRemainingPellets()) {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].Frightened.Enable(pellet.duration);
        }

        AudioManager.Instance.PlayEffect(AudioManager.Instance.PowerPalletEffectAudioClip);

        PelletEaten(pellet, false);

        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets) {
            if (pellet.gameObject.activeSelf) {
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
