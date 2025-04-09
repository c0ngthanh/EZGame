using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    _1v1 = 0,
    _1vMany = 1,
    _ManyvMany = 2
}
public enum GameState
{
    Waiting,
    Play
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameState gameState;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        gameState = GameState.Waiting;
    }
    void Start()
    {
        TryToSpawnUnit(ResourceRef.instance.blueUnitPrefab, new Vector3(0, 200, 0));
    }
    private void TryToSpawnUnit(GameObject unitPrefab, Vector3 position)
    {
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                GameObject unit = Instantiate(unitPrefab, hit.point, Quaternion.identity);
            }
        }
    }
}
