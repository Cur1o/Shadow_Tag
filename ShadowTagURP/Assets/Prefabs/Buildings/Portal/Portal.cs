using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour , IDataPersistance
{
    [SerializeField] private bool isHub;
    [SerializeField] public bool isUnlocked;
    [SerializeField] ScenesManager.Scenes level;
    [SerializeField] ScenesManager.Scenes currentLevel;
    private void Start()
    {
        SaveManager.Instance.dataPersistenceObjects.Add(this);
        LoadData(SaveManager.Instance.gameData);
        if (!isUnlocked)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isHub)
        {
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
        }
    }
    public void SaveData(ref SaveData data) => data.unlockedLevels[(int)currentLevel] = this.isUnlocked;
    public void LoadData(SaveData data) => this.isUnlocked = data.unlockedLevels[(int)currentLevel];
}
