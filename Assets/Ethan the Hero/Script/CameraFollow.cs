using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        
    public Vector3 offset;          
    public float smoothSpeed = 0.125f;

    private Vector3 initialPosition;    // Posici�n inicial de la c�mara
    private Vector3 targetInitialPosition; // Posici�n inicial del target

    void Start()
    {
        initialPosition = transform.position;         
        targetInitialPosition = target.position;  // Guarda la posici�n inicial del player
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calcula cu�nto se ha movido el player respecto a su posici�n inicial
        Vector3 targetDelta = (target.position - targetInitialPosition) + offset;

        // Suma ese delta a la posici�n inicial de la c�mara
        Vector3 desiredPosition = initialPosition + new Vector3(targetDelta.x, targetDelta.y, 0);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, initialPosition.z);
    }
}
