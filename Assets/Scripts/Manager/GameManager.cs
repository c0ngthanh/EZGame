using System;
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
    Play,
    Testing
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;
    public Unit playerUnits;
    public Unit[] playerTeamMateUnits;
    public Unit[] enemyUnits;
    private int gamemode;
    private int level;
    private int playerType;
    private int enemyCount;
    private int playerTeammateCount;
    private Vector3 spawnPosition1 = new Vector3(0, 200, -1.5f);
    private Vector3 spawnPosition2 = new Vector3(0, 200, 1.5f);
    public EventHandler<bool> OnResultEvent;
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
        QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 200;
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
        playerUnits = TryToSpawnUnit(unitAttriBute.GetFaction(), spawnPosition1).GetComponent<Unit>();
        if(playerUnits != null)
        {
            playerUnits.gameObject.AddComponent<PlayerInputController>();
            playerUnits.GetComponent<AIController>().enabled = false;
            playerUnits.GetComponent<Unit>().SetUnitAttribute(unitAttriBute);
        }
        playerTeamMateUnits = new Unit[playerTeammateCount];
        for(int i = 0; i < playerTeammateCount; i++)
        {
            UnitAttriBute unitAttribute = LevelManager.instance.GetUnitData(gamemode, level, getRandomUnitType());
            playerTeamMateUnits[i] = TryToSpawnUnit(Faction.Player, GetRandomSpawnPositionXZ(spawnPosition1,i,playerTeammateCount)).GetComponent<Unit>();
            playerTeamMateUnits[i].SetUnitAttribute(unitAttribute);
        }
        enemyUnits = new Unit[enemyCount];
        for (int i = 0; i < enemyCount; i++)
        {
            UnitAttriBute unitAttribute = LevelManager.instance.GetUnitData(gamemode, level, getRandomUnitType());
            enemyUnits[i] = TryToSpawnUnit(Faction.Enemy, GetRandomSpawnPositionXZ(spawnPosition2,i,enemyCount)).GetComponent<Unit>();
            enemyUnits[i].SetUnitAttribute(unitAttribute);
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
    private Vector3 GetRandomSpawnPositionXZ(Vector3 center, int positionIndex, int positionCount)
    {
        int ring = 0;
        int currentIndex = 0;
        float ringSize = 1f;
        while (currentIndex < positionCount)
        {
            int ringPositionCount = 3 + 2 * ring; // Number of positions in the current ring

            for (int i = 0; i < ringPositionCount; i++)
            {
                float angle = i * Mathf.PI * 2 / ringPositionCount; // Angle for each position in the ring
                Vector3 ringVector = Quaternion.Euler(0, Mathf.Rad2Deg * angle, 0) * new Vector3(ringSize * (ring + 1), 0, 0);
                Vector3 ringPosition = center + ringVector;

                if (currentIndex == positionIndex)
                {
                    return ringPosition; // Return the position at the specified index
                }

                currentIndex++;
                if (currentIndex >= positionCount)
                {
                    break;
                }
            }

            ring++;
        }

        return center; // Fallback to the center if no position is found
    }
    private int getRandomUnitType()
    {
        int randomIndex = UnityEngine.Random.Range(0, LevelManager.instance.units.Length);
        return randomIndex;
    }
    public void GoToMainMenu(){
        //Return Obj back to pool 
        for(int i=0; i<enemyUnits.Length; i++){
            PoolObject.SharedInstance.ReturnPooledObject(enemyUnits[i].gameObject);
        }
        for(int i=0; i<playerTeamMateUnits.Length; i++){
            PoolObject.SharedInstance.ReturnPooledObject(playerTeamMateUnits[i].gameObject);
        }
        Destroy(playerUnits.gameObject.GetComponent<PlayerInputController>());
        playerUnits.gameObject.GetComponent<AIController>().enabled = true;
        PoolObject.SharedInstance.ReturnPooledObject(playerUnits.gameObject);
        gameState = GameState.Waiting;
        SceneManager.LoadScene("MainScene");
    }
    public void CheckGameOver(){
        if(playerUnits.gameObject.activeSelf == false){
            foreach(Unit unit in playerTeamMateUnits){
                if(unit.gameObject.activeSelf == true){
                    return;
                }
            }
            OnResultEvent?.Invoke(this, false);
        }else{
            foreach(Unit unit in enemyUnits){
                if(unit.gameObject.activeSelf == true){
                    return;
                }
            }
            OnResultEvent?.Invoke(this, true);
        }
    }

    public void StartTesting()
    {
        gameState = GameState.Testing;
        SceneManager.LoadScene("50Unit");
    }
}
