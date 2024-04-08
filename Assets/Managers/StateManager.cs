using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    private const string WIN_TEXT = "YOU WIN C:";
    private const string LOSE_TEXT = "YOU LOSE :C";

    private const string START_SCENE = "StartScene";
    private const string LEVEL_1_SCENE = "Level1Scene";
    private const string LEVEL_2_SCENE = "Level2Scene";
    private const string Result_SCENE = "ResultScene";

    private GameState _gameState;
    public static GameState GameState
    {
        get
        {
            return Instance._gameState;
        }

        set
        {
            Instance.MakeDecision(value)();
        }
    }

    private static StateManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void AdvanceLevel()
    {
        GameState = ChooseNextLevel(GameState)();
    }

    private Action MakeDecision(GameState newGameState)
    {
        _gameState = newGameState;

        return newGameState switch
        {
            GameState.START => () => LoadScene(START_SCENE),
            GameState.LEVEL1 => () => LoadScene(LEVEL_1_SCENE),
            GameState.LEVEL2 => () => LoadScene(LEVEL_2_SCENE),
            GameState.WIN => () => LoadResultScene(WIN_TEXT),
            GameState.LOSE => () => LoadResultScene(LOSE_TEXT),
            _ => throw new ArgumentOutOfRangeException(nameof(_gameState), _gameState, null),
        };
    }

    private static Func<GameState> ChooseNextLevel(GameState gameState)
    {
        return gameState switch
        {
            GameState.START => () => GameState.LEVEL1,
            GameState.LEVEL1 => () => GameState.LEVEL2,
            GameState.LEVEL2 => () => GameState.WIN,
            _ => throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null),
        };
    }

    private static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    private static void LoadResultScene(string displayedText)
    {
        ResultScreenManager.EndScreenText = displayedText;
        LoadScene(Result_SCENE);
    }
}

public enum GameState
{
    START,
    LEVEL1,
    LEVEL2,
    WIN,
    LOSE
}
