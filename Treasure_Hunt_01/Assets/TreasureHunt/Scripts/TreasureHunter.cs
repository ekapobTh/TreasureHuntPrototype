using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureHunter : MonoBehaviour
{
    [SerializeField] private List<Sprite> treasureSpriteLists;
    [SerializeField] private GameObject treasurePrefab;
    [SerializeField] private GameObject treasureHintPrefab;
    [SerializeField] private GameObject treasureParent;
    [SerializeField] private GameObject treasureHintParent;
    [SerializeField] private GameObject continueBut;
    public GamePrefabs gamePrefabs;

    string jsonStr = "{ 	\"gameplayObj\" : [ 		{ 			\"name\":\"A_Blank\", 			\"minX\":0.25, 			\"minY\":0.6, 			\"maxX\":0.4, 			\"maxY\":0.8 		}, 		{ 			\"name\":\"B_Blank\", 			\"minX\":0.25, 			\"minY\":0.2, 			\"maxX\":0.4, 			\"maxY\":0.4 		}, 		{ 			\"name\":\"C_Blank\", 			\"minX\":0.6, 			\"minY\":0.2, 			\"maxX\":0.75, 			\"maxY\":0.4 		}, 		{ 			\"name\":\"D_Blank\", 			\"minX\":0.6, 			\"minY\":0.6, 			\"maxX\":0.75, 			\"maxY\":0.8 		} 	] }";
    // Start is called before the first frame update
    void Awake()
    {
        THInitiate();
    }

    private void THInitiate()
    {
        int currentData = 0;
        continueBut.SetActive(false);
        gamePrefabs = new GamePrefabs();
        gamePrefabs = JsonUtility.FromJson<GamePrefabs>(jsonStr);
        foreach (Sprite sprite in treasureSpriteLists)
        {
            SpawnTreasure(sprite, SpawnTreasureHint(sprite), gamePrefabs.gameplayObj[currentData]);
            currentData++;
        }
    }
    private void SpawnTreasure(Sprite sprite, GameObject hintObj, GameplayObj gameplayObj)
    {
        var spawnObj = Instantiate(treasurePrefab) as GameObject;
        var spawnObjScript = spawnObj.GetComponent<Treasure>();
        spawnObj.transform.SetParent(treasureParent.transform);
        spawnObjScript.SetSyncRect(gameplayObj.minX, gameplayObj.minY, gameplayObj.maxX, gameplayObj.maxY);
        spawnObjScript.Initiate();
        spawnObjScript.UpdateRect();
        spawnObjScript.TreasureImage.sprite = sprite;
        spawnObjScript.TreasureName = sprite.name;
        spawnObjScript.HintObj = hintObj;
    }
    private GameObject SpawnTreasureHint(Sprite sprite)
    {
        var spawnObj = Instantiate(treasureHintPrefab) as GameObject;
        var spawnObjScript = spawnObj.GetComponent<TreasureHint>();
        spawnObj.transform.SetParent(treasureHintParent.transform);
        spawnObjScript.Initiate();
        spawnObjScript.TreasureImage.sprite = sprite;
        spawnObjScript.TreasureName = sprite.name;

        return spawnObj;
    }
    public void EndContent()
    {
        continueBut.SetActive(true);
    }
    public void OnClickContinue()
    {
        THInitiate();
    }
}

[System.Serializable]
public class GamePrefabs
{
    public GameplayObj[] gameplayObj;
}

[System.Serializable]
public class GameplayObj
{
    public string name;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
}