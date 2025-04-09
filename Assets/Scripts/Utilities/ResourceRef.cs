using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRef : MonoBehaviour
{
    public static ResourceRef instance;
    [HideInInspector] public GameObject blueUnitPrefab;
    [HideInInspector] public GameObject redUnitPrefab;
    [HideInInspector] public GameObject brownUnitPrefab;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        blueUnitPrefab = Resources.Load("Prefab/BlueUnit") as GameObject;
        redUnitPrefab = Resources.Load("Prefab/RedUnit") as GameObject;
        brownUnitPrefab = Resources.Load("Prefab/BrownUnit") as GameObject;
    }
}
