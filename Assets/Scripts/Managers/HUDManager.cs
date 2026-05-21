using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("Top Bar")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI glassesStateText;

    [Header("Energy Meter")]
    public Slider energyMeter;

    [Header("Feedback")]
    public TextMeshProUGUI feedbackText;
    private float feedbackTimer = 0f;

    [Header("Win/Lose Panels")]
    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI winScoreText;
    public TextMeshProUGUI loseScoreText;

    private int score = 0;
    private float timeRemaining = 60f;
    private bool gameActive = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        gameActive = true;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        UpdateScore(0);
        UpdateEnergyMeter(80f);
    }

    void Update()
    {
        if (!gameActive) return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0f, timeRemaining);
        UpdateTimer();

        if (timeRemaining <= 0f)
            ShowLose();

        if (feedbackTimer > 0f)
        {
            feedbackTimer -= Time.deltaTime;
            if (feedbackTimer <= 0f)
                feedbackText.gameObject.SetActive(false);
        }
    }

    void UpdateTimer()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = seconds + "s";

        if (timeRemaining <= 15f)
            timerText.color = Color.red;
        else
            timerText.color = Color.white;
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;

        if (score >= 80)
            ShowWin();
    }

    public void UpdateEnergyMeter(float value)
    {
        energyMeter.value = value;
    }

    // Decrease energy meter
    public void DecreaseEnergy()
    {
        energyMeter.value -= 20f;
        energyMeter.value = Mathf.Max(0f, energyMeter.value);
    }

    public void UpdateGlassesState(bool isOn)
    {
        glassesStateText.text = "AR Glasses: " + (isOn ? "ON" : "OFF");
        glassesStateText.color = isOn ? Color.green : Color.white;
    }

    public void ShowFeedback(string message)
    {
        feedbackText.gameObject.SetActive(true);
        feedbackText.text = message;
        feedbackTimer = 2f;
    }

    void ShowWin()
    {
        gameActive = false;
        winPanel.SetActive(true);
        winScoreText.text = "Final Score: " + score;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ShowLose()
    {
        gameActive = false;
        losePanel.SetActive(true);
        loseScoreText.text = "Final Score: " + score;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


public void BackToMenu()
{
    Time.timeScale = 1f;
    UnityEngine.SceneManagement.SceneManager
        .LoadScene(UnityEngine.SceneManagement
        .SceneManager.GetActiveScene().name);
}
    public void ResetGame()
{
    score = 0;
    timeRemaining = 60f;
    gameActive = true;
    scoreText.text = "Score: 0";
    energyMeter.value = 80f;
    glassesStateText.text = "AR Glasses: OFF";
    glassesStateText.color = Color.white;
    winPanel.SetActive(false);
    losePanel.SetActive(false);
    feedbackText.gameObject.SetActive(false);
}
}