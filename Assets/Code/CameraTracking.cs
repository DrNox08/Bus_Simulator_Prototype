using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [Header("Player Ref")]
    public Transform player; // Riferimento al Player

    [Header("Camera Settings")]
    [SerializeField] Vector3 offset; // Offset della Camera rispetto al Player
    [SerializeField] float followSpeed = 5f; // Velocità con cui la Camera segue il Player

    Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset; // tiene la posizione del player aggiornata e agginge un offset
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
            

            

            
            
}
