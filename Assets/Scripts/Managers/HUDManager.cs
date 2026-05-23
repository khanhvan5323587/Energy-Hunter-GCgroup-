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

    [Header("Education Panel")]
    public GameObject educationPanel;
    public TextMeshProUGUI educationTitle;
    public TextMeshProUGUI educationFact;

    private bool educationPanelOpen = false;
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
        educationPanel.SetActive(false);
        UpdateScore(0);
        UpdateEnergyMeter(100f);
    }

    void Update()
    {
        // Close education panel on left click
        if (educationPanelOpen &&
            Input.GetMouseButtonDown(0))
        {
            CloseEducation();
            return;
        }

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

        if (score >= 100)
            ShowWin();
    }

    public void UpdateEnergyMeter(float value)
    {
        energyMeter.value = value;
    }

    public void DecreaseEnergy()
    {
        energyMeter.value -= 25f;
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

    public void ShowEducation(string title, string fact)
    {
        educationPanel.SetActive(true);
        educationTitle.text = title;
        educationFact.text = fact;
        educationPanelOpen = true;

        // Pause + unlock cursor
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseEducation()
    {
        educationPanel.SetActive(false);
        educationPanelOpen = false;

        // Only lock cursor if game still active
        if (gameActive)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            // Game ended, keep cursor visible
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void ShowWin()
    {
        gameActive = false;
        educationPanelOpen = false;
        educationPanel.SetActive(false);
        winPanel.SetActive(true);
        winScoreText.text = "Final Score: " + score;

        // Keep timeScale = 1 so buttons work
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        FPS_Controller fps = FindObjectOfType<FPS_Controller>();
        if (fps != null) fps.enabled = false;
    }

    void ShowLose()
    {
        gameActive = false;
        educationPanelOpen = false;
        educationPanel.SetActive(false);
        losePanel.SetActive(true);
        loseScoreText.text = "Final Score: " + score;

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        FPS_Controller fps = FindObjectOfType<FPS_Controller>();
        if (fps != null) fps.enabled = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        educationPanelOpen = false;
        UnityEngine.SceneManagement.SceneManager
            .LoadScene(UnityEngine.SceneManagement
            .SceneManager.GetActiveScene().name);
    }

    public void ResetGame()
    {
        score = 0;
        timeRemaining = 60f;
        gameActive = true;
        educationPanelOpen = false;
        scoreText.text = "Score: 0";
        energyMeter.value = 100f;
        glassesStateText.text = "AR Glasses: OFF";
        glassesStateText.color = Color.white;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        educationPanel.SetActive(false);
        feedbackText.gameObject.SetActive(false);
    }
}