using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour , IDataPersistance
{
    [SerializeField] private bool isHub;
    [SerializeField] public bool isUnlocked;
    [SerializeField] ScenesManager.Scene level;
    [SerializeField] ScenesManager.Scene currentLevel;
    private void Start()
    {
        SaveManager.Instance.dataPersistenceObjects.Add(this);
        LoadData(SaveManager.Instance.gameData);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isHub)
        {
            SaveManager.Instance.gameData.points[SaveManager.Instance.gameData.currentLabyrinthLevel] = SaveManager.Instance.gameData.currentPoints;
            isUnlocked = true;
            SaveManager.Instance.gameData.currentLabyrinthLevel++;
            SaveManager.Instance.gameData.playerPosition = Vector3.zero;
            PlayerUI.Instance.UpdateLevel();
            ScenesManager.Instance.LoadNextScene();
        }
        else if(isHub && isUnlocked)
        {
            SaveManager.Instance.gameData.currentLabyrinthLevel = (int)level;
            SaveManager.Instance.gameData.playerPosition = Vector3.zero;
            PlayerUI.Instance.UpdateLevel();
            ScenesManager.Instance.LoadScene(level);
        }else if (!isUnlocked)
        {
            gameObject.SetActive(false);
        }
    }
    public void SaveData(ref SaveData data) => data.unlockedLevels[(int)currentLevel] = this.isUnlocked;
    public void LoadData(SaveData data) => this.isUnlocked = data.unlockedLevels[(int)currentLevel];
}
