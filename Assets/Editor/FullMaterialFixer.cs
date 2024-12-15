using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FullMaterialFixer : Editor
{
    [MenuItem("Tools/Fix Imported Materials and Prefabs")]
    public static void FixMaterialsAndPrefabs()
    {
        // Fase 1: Trova e correggi tutti i materiali
        Debug.Log("Iniziando la correzione dei materiali...");
        string[] materialGuids = AssetDatabase.FindAssets("t:Material");
        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (material != null)
            {
                // Cambia shader a URP/Lit se è ancora Standard
                if (material.shader.name == "Standard")
                {
                    Texture mainTex = material.GetTexture("_MainTex");
                    Color albedoColor = material.HasProperty("_Color") ? material.GetColor("_Color") : Color.white;

                    // Assegna shader URP/Lit
                    material.shader = Shader.Find("Universal Render Pipeline/Lit");

                    // Reimposta le texture e il colore
                    if (mainTex != null) material.SetTexture("_BaseMap", mainTex);
                    material.SetColor("_BaseColor", albedoColor);

                    Debug.Log($"Materiale convertito: {material.name}");
                }
            }
        }

        // Fase 2: Trova e aggiorna tutti i prefab
        Debug.Log("Iniziando la correzione dei prefab...");
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null)
            {
                Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>(true);
                foreach (Renderer renderer in renderers)
                {
                    Material[] sharedMaterials = renderer.sharedMaterials;

                    for (int i = 0; i < sharedMaterials.Length; i++)
                    {
                        Material material = sharedMaterials[i];
                        if (material != null && material.shader.name == "Standard")
                        {
                            // Crea un clone del materiale per evitare conflitti
                            Material clonedMaterial = Object.Instantiate(material);
                            clonedMaterial.name = material.name + "_URP";

                            // Salva il materiale nella cartella Materiali
                            string materialPath = "Assets/Materials/" + clonedMaterial.name + ".mat";
                            AssetDatabase.CreateAsset(clonedMaterial, materialPath);

                            // Assegna il nuovo materiale
                            sharedMaterials[i] = clonedMaterial;
                        }
                    }

                    // Aggiorna i materiali del renderer
                    renderer.sharedMaterials = sharedMaterials;
                }
            }
        }

        // Salva e aggiorna il progetto
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Correzione completata!");
    }
}
