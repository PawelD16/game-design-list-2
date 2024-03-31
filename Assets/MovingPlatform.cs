using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed = 1.0f;

    private float startTime;
    private float journeyLength;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, endPosition);
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.PingPong(fractionOfJourney, 1));
    }

}
