using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    public Unit[] playerUnits;
    public Unit[] enemyUnits;
    private int gamemode;
    private int level;
    private int playerType;
    private int enemyCount;
    private int playerTeammateCount;
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
    public void SetData(int gamemode, int level, int playerType, int enemyCount, int playerTeammateCount)
    {
        // Set data for player and enemy units based on the selected game mode and level
        this.gamemode = gamemode;
        this.level = level;
        this.playerType = playerType;
        this.enemyCount = enemyCount;
        this.playerTeammateCount = playerTeammateCount;
    }
    public void StartGame()
    {
        // Example: Load the "GameScene" and call OnGameSceneLoaded when complete
        LoadSceneWithCallback("GameScene", OnGameSceneLoaded);
    }

    private void LoadSceneWithCallback(string sceneName, System.Action callback)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, callback));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, System.Action callback)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Invoke the callback after the scene is loaded
        callback?.Invoke();
    }

    private void OnGameSceneLoaded()
    {
        Debug.Log("Game scene loaded successfully!");
        gameState = GameState.Play;
        // enemyUnits = new Unit[enemyCount];
        UnitAttriBute unitAttriBute = LevelManager.instance.GetPlayerData(playerType);
        GameObject player = TryToSpawnUnit(unitAttriBute.GetFaction(), new Vector3(0, 200, 0));
        if(player != null)
        {
            player.AddComponent<PlayerInputController>();
            player.GetComponent<Unit>().SetUnitAttribute(unitAttriBute);
        }
        playerUnits = new Unit[playerTeammateCount];
        for(int i = 0; i < playerTeammateCount; i++)
        {
            UnitAttriBute unitAttribute = LevelManager.instance.GetUnitData(gamemode, level, getRandomUnitType());
            playerUnits[i] = TryToSpawnUnit(Faction.Player, new Vector3(0, 200, 1 + i)).GetComponent<Unit>();
            playerUnits[i].SetUnitAttribute(unitAttribute);
            playerUnits[i].gameObject.AddComponent<AIController>();
        }
        
    }
    private GameObject TryToSpawnUnit(Faction faction, Vector3 position)
    {
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                // GameObject unit = Instantiate(unitPrefab, hit.point, Quaternion.identity);
                GameObject unit = PoolObject.SharedInstance.GetPooledObject(faction);
                unit.SetActive(true);
                unit.transform.SetParent(null);
                unit.transform.position = hit.point;
                return unit;
            }
        }
        return null;
    }
    private int getRandomUnitType()
    {
        int randomIndex = Random.Range(0, LevelManager.instance.units.Length);
        return randomIndex;
    }
}
