using System.Collections;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour {
    [Range(1f, 3f)] [SerializeField] private float gameSpeed = 1f;

// [SerializeField] private int pointsPerBlockedDestroyed = 83;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] public bool play_intro=false;
    [SerializeField] private int levelNumber = 1;

    [SerializeField] private int currentscore = 0;
    [SerializeField] private int lives = 5;
    [SerializeField]  bool isAutoPlayEnabled ;
    private bool PlayingStartAnimation = true;
    public Animator animator;
    private void Awake() {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
        
    }

    public void ResetScore() {
        Destroy(gameObject);
    }


    private void Start() {
        this.UpDateScore();
        if (play_intro == true) {
            animator.SetTrigger("Intro");
        }
       
    }

    // Update is called once per frame
    private void Update() {
        Time.timeScale = gameSpeed;
       

    }

    public bool LooseLife() {
        lives--;
        UpDateScore();
        

        if (lives <= 0) {
            return false;
        } else {
            if (play_intro == true) {
                animator.SetTrigger("Intro");
            }
            return true;
        }
        
    }
    public void UpDateScore() {
        scoreText.text = "Score: " + currentscore.ToString();
        livesText.text = "Lives: " + lives.ToString();
        LevelText.text = "Level " + levelNumber + "\n Get Ready";


    }
    public void AddToScore(int points) {
        currentscore += points;
        this.UpDateScore();
    }
    public bool IsAutoPlayEnabled() {
        return isAutoPlayEnabled;
    }
}