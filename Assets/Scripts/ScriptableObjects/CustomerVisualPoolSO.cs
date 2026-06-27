using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CustomerVisualPoolSO", fileName = "CustomerVisualPoolSO")]
public class CustomerVisualPoolSO : ScriptableObject
{
    public Material[] bodyMaterials;
    public Material[] faceMaterials;
    public GameObject[] hatPrefabs;
}