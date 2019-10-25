using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum CursorSelection
 {
     Nothing = 0,
     Bucket = 1,
     Remover = 2,
     Shield = 3
 }

public enum GameMode
{
    Nothing = 0,
    Tracking = 1,
    Watching = 2
}

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject m_EyeImage;
    [SerializeField] public GameObject m_PlayImage;

    public static CursorSelection SelectionCursor = CursorSelection.Nothing;
 
    public GameMode currentGameMode = GameMode.Nothing;

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
            timerTxt.text = ((int)timer).ToString();

        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;

            if (timer <= 0)
                WatchingMode();
        }

        if (Input.GetButtonDown("Cancel"))
            PauseGame();
    }
    #endregion

    #region Score
    public void Scoring()
    {
        score += 100;
        if (scoreTxt.IsActive())
            scoreTxt.text = score.ToString();
    }
    #endregion
    
    #region Musics
    [Header("Musics")]
    [SerializeField] private AudioClip mainMenuMusic;
    #endregion

    #region Panels
    [Header("Panels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject levelCompleteUI;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;

    [Header("Live - Texts")]
    [SerializeField] Text timerTxt;

    [Header("Live - Score")]
    [SerializeField] Text scoreTxt;
    [SerializeField] Text HighScoreTxt;
    private int score;
    [Header("Live - Score")]
    [SerializeField] Text jourTxt;

    private List<GameObject> panels;

    public bool FirstGame { get; set; } = false;

    private void RegisterPanels()
    {
        panels = new List<GameObject>();
        panels.Add(mainMenuPanel);
        panels.Add(levelPanel);
        panels.Add(levelCompleteUI);
        panels.Add(pausePanel);
        panels.Add(gameOverPanel);
        panels.Add(winPanel);
    }

    private void DisablePanels()
    {
        foreach (GameObject panel in panels)
            panel.SetActive(false);
    }
    #endregion

    #region Time
    private float timer = 0f;

    private void SetTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }
    #endregion

    #region Game Flow

    public void InitGame()
    {
        SetTimeScale(0);

        DisablePanels();
        SoundManager.instance.PlayMusic(mainMenuMusic);
        FirstGame = true;
        mainMenuPanel.SetActive(true);
    }

    public void StartLevel()
    {
        currentGameMode = GameMode.Tracking;

        FindObjectOfType<GameManager>().m_PlayImage.SetActive(false);
        FindObjectOfType<GameManager>().m_EyeImage.SetActive(true);

        FindObjectOfType<Level>().InitLevel(FirstGame);
        timer = FindObjectOfType<Level>().LevelDuration;
        
        FindObjectOfType<PlayerGaze>().gameObject
            .GetComponent<SpriteRenderer>().enabled = true;

        FindObjectOfType<Drone>().SeenFiresPosition.Clear();
        
        DisablePanels();
        levelPanel.SetActive(true);

        SetTimeScale(1);
    }

    public void WatchingMode()
    {
        currentGameMode = GameMode.Watching;

        FindObjectOfType<GameManager>().m_PlayImage.SetActive(true);
        FindObjectOfType<GameManager>().m_EyeImage.SetActive(false);

        if (FindObjectOfType<PlayerGaze>())
            FindObjectOfType<PlayerGaze>().gameObject
                .GetComponent<SpriteRenderer>().enabled = false;
        FindObjectOfType<Drone>().Move();
    }

    public void GoToNextLevel()
    {
        foreach (var s in FindObjectsOfType<Smoke>())
            Destroy(s.gameObject);
        DisablePanels();
        
        if (FindObjectsOfType<ForestTree>().Length <= 10)
            GameOver();
        else 
            levelCompleteUI.SetActive(true);
    }

    private int currentLevelIndex = 0;

    public void NextLevel()
    {
        currentLevelIndex++;
        jourTxt.text = currentLevelIndex.ToString();
        StartLevel();
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
        ClearScene();
        HighScoreTxt.text = score.ToString();
        score = 0;
        DisablePanels();
        SetTimeScale(0);

        gameOverPanel.SetActive(true);
    }

    public void WinGame()
    {
        ClearScene();
        score = 0;
        DisablePanels();
        SetTimeScale(0);
        
        winPanel.SetActive(true);
    }

    private void ClearScene()
    {
        foreach (var f in FindObjectsOfType<Fire>())
            Destroy(f.gameObject);
        foreach (var s in FindObjectsOfType<Smoke>())
            Destroy(s.gameObject);
        foreach (var fo in FindObjectsOfType<ForestTree>())
            Destroy(fo.gameObject);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    /*#region Buttons

    public void WaterBucket_OnClick()
    {
        if(SelectionCursor != CursorSelection.Bucket || SelectionCursor == CursorSelection.Nothing)
        {
            string texture = "Assets/Resources/Sprites/BucketSprite.png";
            Texture2D foo = (Texture2D)AssetDatabase.LoadAssetAtPath(texture, typeof(Texture2D));
            Cursor.SetCursor(foo, new Vector2(20, 20), CursorMode.ForceSoftware);
            SelectionCursor = CursorSelection.Bucket;
        }
        else
        {
            SelectionCursor = CursorSelection.Nothing;
            Cursor.SetCursor(null, new Vector2(20, 20), CursorMode.ForceSoftware);
        }
    }

    public void TreeRemover_OnClick()
    {
        if (SelectionCursor != CursorSelection.Remover || SelectionCursor == CursorSelection.Nothing)
        {
            string texture = "Assets/Resources/Sprites/TreeRemoverSprite.png";
            Texture2D foo = (Texture2D)AssetDatabase.LoadAssetAtPath(texture, typeof(Texture2D));
            Cursor.SetCursor(foo, new Vector2(20, 20), CursorMode.ForceSoftware);
            SelectionCursor = CursorSelection.Remover;
        }
        else
        {
            SelectionCursor = CursorSelection.Nothing;
            Cursor.SetCursor(null, new Vector2(20, 20), CursorMode.ForceSoftware);
        }
    }

    public void Shield_OnClick()
    {
        if(SelectionCursor != CursorSelection.Shield || SelectionCursor == CursorSelection.Nothing)
        {
            string texture = "Assets/Resources/Sprites/ShieldSprite.png";
            Texture2D foo = (Texture2D)AssetDatabase.LoadAssetAtPath(texture, typeof(Texture2D));
            Cursor.SetCursor(foo, new Vector2(20, 20), CursorMode.ForceSoftware);
            SelectionCursor = CursorSelection.Shield;
        }
        else
        {
            SelectionCursor = CursorSelection.Nothing;
            Cursor.SetCursor(null, new Vector2(20, 20), CursorMode.ForceSoftware);
        }
    }
    #endregion*/

}
