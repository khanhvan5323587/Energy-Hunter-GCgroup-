using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject howToPlayPanel;
    public GameObject crosshair;
    public MonoBehaviour fpsController;

    void Start()
    {
        menuPanel.SetActive(true);
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
        if (crosshair != null)
            crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;

        if (fpsController != null)
            fpsController.enabled = false;
    }

    public void StartGame()
    {
        menuPanel.SetActive(false);
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
        if (crosshair != null)
            crosshair.SetActive(true);

        // Reset cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Resume game
        Time.timeScale = 1f;

        // Enable FPS
        if (fpsController != null)
            fpsController.enabled = true;

        // Reset HUD
        if (HUDManager.Instance != null)
            HUDManager.Instance.ResetGame();
    }

    public void ShowHowToPlay()
    {
        menuPanel.SetActive(false);
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}