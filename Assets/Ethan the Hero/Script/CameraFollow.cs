using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        
    public Vector3 offset;          
    public float smoothSpeed = 0.125f;

    private Vector3 initialPosition;    // Posición inicial de la cámara
    private Vector3 targetInitialPosition; // Posición inicial del target

    void Start()
    {
        initialPosition = transform.position;         
        targetInitialPosition = target.position;  // Guarda la posición inicial del player
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calcula cuánto se ha movido el player respecto a su posición inicial
        Vector3 targetDelta = (target.position - targetInitialPosition) + offset;

        // Suma ese delta a la posición inicial de la cámara
        Vector3 desiredPosition = initialPosition + new Vector3(targetDelta.x, targetDelta.y, 0);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, initialPosition.z);
    }
}
