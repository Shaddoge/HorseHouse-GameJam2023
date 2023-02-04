using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;
    public void OnPlayPressed()
    {
        SceneManager.LoadScene(1);
    }
    public void OnQuitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnPausePressed()
    {
        if(!pausePanel.activeSelf) { 
            if (GameManager.Instance.State != GameState.GameOver)
            {
                pausePanel.SetActive(true);
            }   
            Time.timeScale = 0.0f;
            EventManager.Instance.GameStateChange(GameState.Paused);
        }
    }

    public void OnResumePressed()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
            EventManager.Instance.GameStateChange(GameState.None);
        }
    }
}
