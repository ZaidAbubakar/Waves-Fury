using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager Instance { get; private set; }

    //UI
    public TextMeshProUGUI ammoDispaly;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
           
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            
        }
    }
}
