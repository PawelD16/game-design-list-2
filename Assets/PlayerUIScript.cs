using TMPro;
using UnityEngine;

public class PlayerUIScript : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI levelText;

    private const string HEALTH_TAG = "Health: ";
    private const string LEVEL_TAG = "Level: ";

    private static PlayerUIScript Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateHealth(Player.GetPlayerHealth());
        UpdateLevel();
    }

    public static void UpdateHealth(int health)
    {
        Instance.healthText.text = HEALTH_TAG + health;
    }

    public static void UpdateLevel()
    {
        Instance.levelText.text = LEVEL_TAG + GetCurrentLevel(StateManager.GameState);
    }

    private static int GetCurrentLevel(GameState gameState)
    {
        return gameState switch
        {
            GameState.LEVEL1 => 1,
            GameState.LEVEL2 => 2,
            _ => -1
        };
    }

}
