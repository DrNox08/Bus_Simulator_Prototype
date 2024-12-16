using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    Camera mainCamera;
    Plane[] frustumPlanes;
    List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    [SerializeField] int frameInterval;
    int frameCount = 0;

    void Start()
    {
        mainCamera = Camera.main;


        MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>();
        meshRenderers.AddRange(renderers);
    }

    void Update()
    {
        frameCount++;

        if (frameCount % frameInterval == 0) { UpdateMeshVisibility(); } // setting the operation to run every X frames
    }

    void UpdateMeshVisibility()
    {
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        foreach (var renderer in meshRenderers)
        {
            // Espandi il bounding box dell'oggetto
            Bounds expandedBounds = renderer.bounds;
            expandedBounds.Expand(10f); // Espande il bounding box di 1 unità

            bool isVisible = GeometryUtility.TestPlanesAABB(frustumPlanes, expandedBounds);
            renderer.enabled = isVisible;
        }
    }
}
