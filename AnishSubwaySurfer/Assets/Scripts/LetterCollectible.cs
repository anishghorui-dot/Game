using UnityEngine;

public class LetterCollectible : MonoBehaviour
{
    public int letterIndex; // 0=A, 1=N, 2=I, 3=S, 4=H
    public float rotationSpeed = 100f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 2f;
    
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        // Rotate
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
        // Float up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
