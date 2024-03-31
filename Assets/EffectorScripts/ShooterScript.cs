using System.Collections;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public const float SHOOTING_INTERVAL = 2f;
    public const float BULLET_SPEED = 5f;

    private Vector3 targetPosition;

    private void Start()
    {
        StartCoroutine(ShootAtIntervals(SHOOTING_INTERVAL));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Player.PLAYER_TAG))
            return;

        Destroy(gameObject);
    }

    private IEnumerator ShootAtIntervals(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            targetPosition = Player.GetTransform().position;
            Shoot(targetPosition);
        }
    }

    private void Shoot(Vector3 target)
    {
        GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        StartCoroutine(MoveProjectile(bullet, target));
    }

    private IEnumerator MoveProjectile(GameObject bullet, Vector3 target)
    {
        while (bullet != null)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target, BULLET_SPEED * Time.deltaTime);

            if (bullet.transform.position == target)
                Destroy(bullet);

            yield return null;
        }
    }
}
