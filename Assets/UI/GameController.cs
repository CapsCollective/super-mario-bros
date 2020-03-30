using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum LevelState { Title, Loading, Main, GameOver };
    public LevelState state;

    public Canvas LoadingCanvas;
    public Canvas UICanvas;

    public LivesManager LM;
    public ScoreManager SM;
    public TimeManager TM;
    public CoinManager CM;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        state = GetLevelState();
        LM = gameObject.GetComponent<LivesManager>();
        SM = gameObject.GetComponent<ScoreManager>();
        TM = gameObject.GetComponent<TimeManager>();
        CM = gameObject.GetComponent<CoinManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (state == LevelState.Title)
        {
            SetUpTitle();
        }
    }

    private LevelState GetLevelState()
    {
        string levelName = SceneManager.GetActiveScene().name;
        if (levelName == "TitleScene")
        {
            return LevelState.Title;
        }
        else if (levelName == "LoadingScene")
        {
            return LevelState.Loading;
        }
        else if (levelName == "MainScene")
        {
            return LevelState.Main;
        }
        else if (levelName == "GameOverScene")
        {
            return LevelState.GameOver;
        }

        return LevelState.Title;
    }

    public void LoadLoadingScene()
    {
        state = LevelState.Loading;
        LoadScene();
        ShowLoadingCanvas();
        TM.ToggleTimerText(false);
        ShowUICanvas();
        //Invoke("LoadMainScene", 2f);
        Invoke("LoadGameOverScene", 2f);
    }

    public void ShowLoadingCanvas()
    {
        LM.UpdateLivesText();
        LoadingCanvas.enabled = true;
    }

    private void LoadMainScene()
    {
        state = LevelState.Main;
        LoadScene();
        LoadingCanvas.enabled = false;
        TM.ToggleTimerText(true);
        ShowUICanvas();
    }

    private void LoadGameOverScene()
    {
        state = LevelState.GameOver;
        LoadScene();
        LoadingCanvas.enabled = false;
        ShowUICanvas();
        //TODO: Play GameOver Audio
        //SoundGuy.Instance.PlaySound("smb_gameover");
        Invoke("LoadTitleScene", 2f);
    }

    public void LoadTitleScene()
    {
        state = LevelState.Title;
        LoadScene();
        SetUpTitle();
        Destroy(gameObject);
    }

    public void ShowUICanvas()
    {
        SM.UpdateScoreText();
        CM.UpdateCoinText();
        UICanvas.enabled = true;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene((int)state);
    }

    public void SetUpTitle()
    {
        ResetAllStats();
        ShowUICanvas();
        TM.ToggleTimerText(false);
        LoadingCanvas.enabled = false;

    }

    public void ResetAllStats()
    {
        TM.ResetTimer();
        SM.ResetScore();
        CM.ResetCoinCount();
        LM.ResetLives();
    }

    public void GameOver()
    {
        SM.SaveHighScore();
        LoadGameOverScene();
    }

    
}
