using UnityEngine;
using UnityEngine.UI;

public class PauseResume : MonoBehaviour
{
    private bool _ispaused = false;
    public Button PauseResumeButton;
    public Text ButtonText;
    public Sprite PauseSprite;
    public Sprite ResumeSprite;

    [SerializeField] GameObject pauseMenu;

    private void Start()
    {
        PauseResumeButton.onClick.AddListener(TogglePause);
        UpdateButtonImage();
    }

    public void TogglePause()
    {
        _ispaused = !_ispaused;

        if (_ispaused)
        {
            Time.timeScale = 0;
            GameManager.Instance.M_PlayerMovements.canControl = false;
            GameManager.Instance.paused = true;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            GameManager.Instance.M_PlayerMovements.canControl = true;
            GameManager.Instance.paused = false;
            pauseMenu.SetActive(false);
        }

        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        PauseResumeButton.image.sprite = _ispaused ? ResumeSprite : PauseSprite;
    }
}
