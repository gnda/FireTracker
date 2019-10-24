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
    [SerializeField] GameObject m_ImageWaterBucket;
    [SerializeField] GameObject m_ImageTreeRemover;
    [SerializeField] GameObject m_ImageShield;

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
        currentGameMode = GameMode.Tracking;
        FindObjectOfType<PlayerGaze>().gameObject
            .GetComponent<SpriteRenderer>().enabled = true;
        DisablePanels();
        timer = FindObjectOfType<Level>().LevelDuration;
        levelPanel.SetActive(true);

        SetTimeScale(1);
    }

    public void WatchingMode()
    {
        currentGameMode = GameMode.Watching;
        FindObjectOfType<PlayerGaze>().gameObject
            .GetComponent<SpriteRenderer>().enabled = false;
        FindObjectOfType<Drone>().Move();
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

    #region Buttons

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
    #endregion

}
