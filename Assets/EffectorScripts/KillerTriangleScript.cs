using System.Collections;
using UnityEngine;

public class KillerTriangleScript : MonoBehaviour
{
    private bool isPlayerTouching = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerTouching = true;
            StartCoroutine(ApplyEffectEverySecond());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.PLAYER_TAG))
            isPlayerTouching = false;
    }

    private IEnumerator ApplyEffectEverySecond()
    {
        while (isPlayerTouching)
        {
            Player.DecreasePlayerHealth();
            yield return new WaitForSeconds(1);
        }
    }
}
