using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    Camera mainCamera;
    Plane[] frustumPlanes;
    List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    [SerializeField] int frameInterval; // intervallo: serve a decidere ogni quanti frame eseguire funzioni in update
    int frameCount = 0; // per il conteggio dei frame

    void Start()
    {
        mainCamera = Camera.main;

        // Trova tutti i MeshRenderer nella scena
        MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>();
        meshRenderers.AddRange(renderers);
    }

    void Update()
    {
        frameCount++;

        if (frameCount % frameInterval == 0) { UpdateMeshVisibility(); }
    }

    void UpdateMeshVisibility()
    {
        // calcola i piani del frustum della telecamera
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        // cicla i meshrenderer e li attiva/disattiva basandosi sul frustum
        foreach (var renderer in meshRenderers)
        {
            bool isVisible = GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
            renderer.enabled = isVisible;
        }
    }
}
