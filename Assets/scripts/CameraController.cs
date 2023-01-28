using UnityEngine;

public class CameraController : MonoBehaviour
{
    public MapController player;

    public float smoothTime;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    private void Awake()
    {
        player = FindObjectOfType<MapController>();
    }

    private void Update()
    {
        // Set position and rotation to interpolate towards player, accounting for the offset
        transform.position = Vector3.Lerp(transform.position, player.transform.position + positionOffset, smoothTime * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(player.transform.rotation.eulerAngles + rotationOffset), smoothTime * Time.deltaTime);
    }
}
