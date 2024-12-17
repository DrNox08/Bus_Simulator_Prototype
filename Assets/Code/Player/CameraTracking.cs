using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [Header("Player Ref")]
    public Transform player; 

    [Header("Camera Settings")]
    [SerializeField] Vector3 offset; 
    [SerializeField] float followSpeed = 5f; 

    Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset; 
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
            

            

            
            
}
