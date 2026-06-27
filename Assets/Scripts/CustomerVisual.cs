using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private Transform hatParent;
    
    [Header("Asset Pools")]
    [SerializeField] private CustomerVisualPoolSO customerVisualPoolSO;

    private void Awake()
    {
        RandomizeVisuals();
    }

    private void RandomizeVisuals()
    {
        // Randomize body material
        Material[] currentMaterials = meshRenderer.materials;

        if (customerVisualPoolSO.bodyMaterials.Length > 0)
        {
            // Index 0 is the Body material
            currentMaterials[0] = customerVisualPoolSO.bodyMaterials[Random.Range(0, customerVisualPoolSO.bodyMaterials.Length)];
        }
        meshRenderer.materials = currentMaterials;

        if (customerVisualPoolSO.hatPrefabs.Length > 0)
        {
            int randomHatIndex = Random.Range(0, customerVisualPoolSO.hatPrefabs.Length + 1);

            if (randomHatIndex < customerVisualPoolSO.hatPrefabs.Length)
            {
                Instantiate(customerVisualPoolSO.hatPrefabs[randomHatIndex], hatParent);
            }
        }
    }

    public void SetReaction(bool isHappy)
    {
        // Face 1 (Happy) -> Index 0 | Face 2 (Angry) -> Index 1
        SetFace(isHappy ? 0 : 1);
    }

    private void SetFace(int index)
    {
        if (customerVisualPoolSO.faceMaterials.Length > index)
        {
            Material[] currentMaterials = meshRenderer.materials;
            currentMaterials[1] = customerVisualPoolSO.faceMaterials[index];
            meshRenderer.materials = currentMaterials;
        }
    }
}