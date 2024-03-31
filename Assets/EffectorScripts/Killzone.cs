using UnityEngine;

public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.PLAYER_TAG))
            Player.KillPlayer();
    }
}
