using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public enum LevelState { Title, Loading, Main, GameOver};
    public LevelState state;

    private void Awake()
    {
        GetLevelState();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName(name).buildIndex);
        state = GetLevelState();
    }
}
