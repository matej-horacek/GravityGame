using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;

    private float fixedX;
    private float fixedY;

    void Start()
    {
        fixedX = transform.position.x;
        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(fixedX, fixedY, (player.position.z - 250f));
    }
}
