using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float gameDuration = 60f;
    private float remainingTime;
    private bool gameEnded = false;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public GameObject endPanel;
    public TextMeshProUGUI finalScoreText;
    public Transform inventoryContent;
    public GameObject inventoryItemPrefab;
    public TextMeshProUGUI score1Text;
    public TextMeshProUGUI score2Text;
    public TextMeshProUGUI score3Text;

    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        remainingTime = gameDuration;
        endPanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        remainingTime -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(remainingTime).ToString();

        if (remainingTime <= 0)
        {
            EndGame();
        }
    }
    
    void SaveScore(int newScore)
    {
        int prev1 = PlayerPrefs.GetInt("Score1", 0);
        int prev2 = PlayerPrefs.GetInt("Score2", 0);

        PlayerPrefs.SetInt("Score1", newScore);
        PlayerPrefs.SetInt("Score2", prev1);
        PlayerPrefs.SetInt("Score3", prev2);

        PlayerPrefs.Save();
    }


    void DisplayPreviousScores()
    {
        int s1 = PlayerPrefs.GetInt("Score1", 0);
        int s2 = PlayerPrefs.GetInt("Score2", 0);
        int s3 = PlayerPrefs.GetInt("Score3", 0);

        score1Text.text = $"Score 1 : {s1}";
        score2Text.text = $"Score 2 : {s2}";
        score3Text.text = $"Score 3 : {s3}";
    }


    void EndGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;
        AudioManager.Instance.PlayGameOver();
        endPanel.SetActive(true);
        finalScoreText.text = $"Score : {ScoreManager.Instance.Score}";

        foreach (Transform child in inventoryContent)
            Destroy(child.gameObject);

        foreach (var kvp in InventoryManager.Instance.GetInventory())
        {
            GameObject item = Instantiate(inventoryItemPrefab, inventoryContent);
            item.GetComponentInChildren<Image>().sprite = kvp.Key.visualPrefab.GetComponent<SpriteRenderer>().sprite;
            item.GetComponentInChildren<TextMeshProUGUI>().text = $"x{kvp.Value}";
        }
        
        int finalScore = ScoreManager.Instance.Score;
        SaveScore(finalScore);
        DisplayPreviousScores();
    }
    
    public void Replay()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}