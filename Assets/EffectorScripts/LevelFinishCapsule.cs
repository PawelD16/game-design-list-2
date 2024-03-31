using UnityEngine;

public class LevelFinishCapsule : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.PLAYER_TAG))
            StateManager.AdvanceLevel();
    }
}
