using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Player.PLAYER_TAG))
            return;

        Player.IncreasePlayerHealth();
        Destroy(gameObject);
    }
}
