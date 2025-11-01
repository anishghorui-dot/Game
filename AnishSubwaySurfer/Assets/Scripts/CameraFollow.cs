using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    
    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 10f;
    public float lookAheadDistance = 5f;
    
    [Header("Tilt Settings")]
    public float tiltAngle = 10f;
    public float tiltSpeed = 5f;
    
    private Vector3 velocity = Vector3.zero;
    private float currentTilt = 0f;
    
    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }
    
    void LateUpdate()
    {
        if (target == null) return;
        
        // Calculate desired position
        Vector3 desiredPosition = target.position + offset;
        
        // Smooth follow
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / smoothSpeed);
        transform.position = smoothedPosition;
        
        // Look at point ahead of player
        Vector3 lookAtPoint = target.position + Vector3.forward * lookAheadDistance;
        transform.LookAt(lookAtPoint);
        
        // Apply tilt based on player lane change (optional enhancement)
        float targetTilt = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            targetTilt = tiltAngle;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            targetTilt = -tiltAngle;
        }
        
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, tiltSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentTilt);
    }
}
