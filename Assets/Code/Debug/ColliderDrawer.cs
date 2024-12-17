using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class ColliderDrawer : MonoBehaviour
{
    [SerializeField]
     Color gizmoColor = Color.green; // Colore configurabile dall'Inspector


    #region Gizmos
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {

        
        
            Gizmos.color = gizmoColor;

            // Draw the Collider based on its type
            if (GetComponent<Collider>() is BoxCollider boxCollider)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawCube(boxCollider.center, boxCollider.size);
                Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
            }
            else if (GetComponent<Collider>() is SphereCollider sphereCollider)
            {
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawSphere(sphereCollider.center, sphereCollider.radius);
                Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius);
            }
           

            Gizmos.matrix = Matrix4x4.identity; // Reset matrix
        

       
    }

    
#endif
    #endregion

}
