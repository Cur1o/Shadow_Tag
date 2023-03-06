using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    [Header("File storage Config")]
    [SerializeField] private string fileName;
    private FileSaveDataHandler dataHandler;
    public static SaveManager Instance { get; private set;}
    //Variables
    public SaveData gameData;
    public List<IDataPersistance> dataPersistenceObjects = new();
    
    private void Awake()
    {
        if (Instance != null || Instance == this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        this.dataHandler = new FileSaveDataHandler(Application.persistentDataPath, fileName);
        LoadGame();
    }
    private void Start()
    {
       
    }
    public void NewGame()
    {
        this.gameData = new SaveData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            NewGame();
        }
    }
    public void SaveGame()
    {
        foreach (IDataPersistance dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

}
