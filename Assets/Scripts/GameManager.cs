using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region MonoBehaviour lifecycle
    protected void Awake()
    {
        RegisterPanels();
    }
    
    void Start()
    {
        InitGame();
    }
    
    private void FixedUpdate()
    {
        if (timerTxt.IsActive())
            timerTxt.text = ((int) timer).ToString();
        
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            
            if (timer <= 0)
                GameOver();
        }

        if (Input.GetButtonDown("Cancel"))
            PauseGame();
    }
    #endregion
    
    #region Panels
        [Header("Panels")]
        [SerializeField] GameObject mainMenuPanel;
        [SerializeField] GameObject levelPanel;
        [SerializeField] GameObject pausePanel;
        [SerializeField] GameObject gameOverPanel;

        [Header("Live - Texts")]
        [SerializeField] Text timerTxt;

        private List<GameObject> panels;

        private void RegisterPanels()
        {
            panels = new List<GameObject>();
            panels.Add(mainMenuPanel);
            panels.Add(levelPanel);
            panels.Add(pausePanel);
            panels.Add(gameOverPanel);
        }
        
        private void DisablePanels()
        {
            foreach (GameObject panel in panels)
            {
                panel.SetActive(false);
            }
        }
    #endregion
    
    #region Time
        private float timer = 0f;
        
    private void SetTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }
    #endregion
    
    #region Levels
    [SerializeField] GameObject[] levelsPrefabs;
    
    private int currentLevelIndex = 0;
    #endregion

    #region Game Flow

    public void InitGame()
    {
        SetTimeScale(0);
        
        DisablePanels();
        mainMenuPanel.SetActive(true);
    }
    
    public void StartLevel()
    {
        DisablePanels();
        timer = FindObjectOfType<Level>().LevelDuration;
        levelPanel.SetActive(true);
        
        SetTimeScale(1);
    }
    
    public void NextLevel()
    {
        if (currentLevelIndex < levelsPrefabs.Length)
            currentLevelIndex++;
        else
            WinGame();
    }
    
    public void PauseGame()
    {
        SetTimeScale(0);
        
        DisablePanels();
        pausePanel.SetActive(true);
    }
    
    public void ResumeGame()
    {
        DisablePanels();
        levelPanel.SetActive(true);
        
        SetTimeScale(1);
    }

    public void GameOver()
    {
        SetTimeScale(0);
        
        gameOverPanel.SetActive(true);
    }
    
    public void WinGame()
    {
        //Application.Quit();
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
