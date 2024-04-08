using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreenManager : MonoBehaviour
{
    public Button restartButton;
    public TextMeshProUGUI endScreenInfo;

    public static string EndScreenText { get; set; }

    private void Start()
    {
        restartButton.onClick.AddListener(ResetGame);
        endScreenInfo.text = EndScreenText;
    }

    private void ResetGame()
    {
        StateManager.GameState = GameState.START;
    }
}
