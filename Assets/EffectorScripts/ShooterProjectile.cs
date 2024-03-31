using UnityEngine;

public class ShooterProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Player.PLAYER_TAG))
            return;

        Player.DecreasePlayerHealth();
        Destroy(gameObject);
    }
}
