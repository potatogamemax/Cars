using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] float rotationSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    // Update is called once per frame
    void Update()
    {
      Vector3 desiredPosition = player.TransformPoint(offset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        Quaternion desiredRotation = Quaternion.LookRotation(player.forward,Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
