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
    }
    public void NewGame() =>this.gameData = new SaveData(); //Creates an instance of SaveData
    
    public void LoadGame()
    {
        this.gameData = dataHandler.Load(); //Gets the saved Data from json file
        if (this.gameData == null)          //If not exist starts a new Game
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
