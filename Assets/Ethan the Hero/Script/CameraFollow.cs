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

        // Posición deseada: siempre centrada en el jugador (+ offset si querés levantarla o alejarla)
        Vector3 desiredPosition = target.position + offset;

        // Interpolación suave
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Mantener la cámara mirando al jugador en XY y conservar el Z de la cámara
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
