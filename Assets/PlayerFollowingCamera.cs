using UnityEngine;

public class PlayerFollowingCamera : MonoBehaviour
{
    public Transform player;

    public uint cameraOffsetX = 0;
    public uint cameraOffsetY = 1;
    public uint cameraOffsetZ = 5;

    private readonly Vector3 cameraOffsetVector;

    public PlayerFollowingCamera()
    {
        cameraOffsetVector = new(cameraOffsetX, cameraOffsetY, -cameraOffsetZ);
    }

    void Update()
    {
        transform.position = player.transform.position + cameraOffsetVector;
    }
}
