using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences Instance { get; private set; }

    public GameObject bulletImpactEffectPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //Debug.Log("GlobalReferences instance set.");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            //Debug.Log("Duplicate GlobalReferences instance destroyed.");
        }
    }
}
