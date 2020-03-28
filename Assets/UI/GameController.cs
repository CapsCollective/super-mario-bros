using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum LevelState { Title, Loading, Main, GameOver};
    public LevelState state;

    public Canvas LoadingCanvas;
    public Canvas UICanvas;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        state = GetLevelState();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (state == LevelState.Loading)
        {
            Invoke("loadMainScene", 2f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private LevelState GetLevelState()
    {
        string levelName = SceneManager.GetActiveScene().name;
        if(levelName == "TitleScene")
        {
            return LevelState.Title;
        }
        else if(levelName == "LoadingScene")
        {
            return LevelState.Loading;
        }
        else if(levelName == "MainScene")
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
        LoadingCanvas.enabled = true;
        LoadScene();
    }

    private void loadMainScene()
    {
        state = LevelState.Main;
        LoadingCanvas.enabled = false;
        LoadScene();
        
    }
    public void LoadScene()
    {
        SceneManager.LoadScene((int)state);
    }
}
