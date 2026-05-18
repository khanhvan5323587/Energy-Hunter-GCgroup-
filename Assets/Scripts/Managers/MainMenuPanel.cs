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
        howToPlayPanel.SetActive(false);
        if (crosshair != null)
            crosshair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;

        if (fpsController != null)
            fpsController.enabled = true;
    }

    public void ShowHowToPlay()
    {
        menuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        howToPlayPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}