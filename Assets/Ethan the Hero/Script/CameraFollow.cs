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

        // Posici�n deseada: siempre centrada en el jugador (+ offset si quer�s levantarla o alejarla)
        Vector3 desiredPosition = target.position + offset;

        // Interpolaci�n suave
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Mantener la c�mara mirando al jugador en XY y conservar el Z de la c�mara
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
