using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [Header("File storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private string settingsFileName;
    private FileSaveDataHandler dataHandler;
    private FileSaveDataHandler dataHandlerSettings;

    public static SaveManager Instance { get; private set;}
    public bool useEncryption = false;  //From Tutorial https://www.youtube.com/watch?v=gMfijpTkKX8
    public static bool EncryptSaveFile = false; //From Tutorial https://www.youtube.com/watch?v=gMfijpTkKX8
    public SaveData gameData;
    public SaveSettings settingsData;
    public List<IDataPersistance> dataPersistenceObjects = new();
    public List<IDataPersistanceSettings> dataPersistenceObjectsSettings = new();
    private void Awake()
    {
        if (Instance != null || Instance == this)
            Destroy(gameObject);
        else
            Instance = this;
        EncryptSaveFile = useEncryption;    //From Tutorial https://www.youtube.com/watch?v=gMfijpTkKX8
        this.dataHandler = new FileSaveDataHandler(Application.persistentDataPath, fileName);
        this.dataHandlerSettings = new FileSaveDataHandler(Application.persistentDataPath, settingsFileName);
        LoadSettings();
    }
    ///<summary>
    ///Loads the saved game data from the file, if it exists. If the data file does not exist, a new game is started.
    ///</summary>
    public void LoadGame()
    {
        this.gameData = dataHandler.Load(); //Gets the saved Data from json file
        if (this.gameData == null) NewGame(); //If not exist starts a new Game 
    }
    ///<summary>
    ///Loads the saved settings data from the file, if it exists. If the data file does not exist, the standard settings are used.
    ///</summary>
    private void LoadSettings()
    {
        this.settingsData = dataHandlerSettings.LoadSettings(); //Gets the saved Data from json file
        if (this.settingsData == null) NewSettings(); //If not exist starts a new Game
    }
    /// <summary>
    /// Resets the variables in the current saved data to the defauklt states.
    /// </summary>
    public void NewGame() =>this.gameData = new SaveData(); //Creates an instance of SaveData
    /// <summary>
    /// Resets the variables in the current saved settings data to the defauklt states, And Load these into the settings menu.
    /// </summary>
    public void NewSettings()
    {
        this.settingsData = new SaveSettings();
        Settings.Instance.LoadSettings(this.settingsData);
    }
    /// <summary>
    /// Calls all IDataPersistance Objects and the save functions there to save the Game Data
    /// </summary>
    public void SaveGame()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "Menu")
        {
            foreach (IDataPersistance dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(ref gameData);
            }
            dataHandler.Save(gameData);
        }
    }
    /// <summary>
    /// Calls all IDataPersistanceSettings Objects and the save functions there to save the Settings Data
    /// </summary>
    public void SaveSettings()
    {
            foreach (IDataPersistanceSettings dataPersistenceObjSettings in dataPersistenceObjectsSettings)
            {
                dataPersistenceObjSettings.SaveSettings(ref settingsData);
            }
            dataHandlerSettings.SaveSettings(settingsData);
    }
    /// <summary>
    /// If the Game is going to close the game Data is saved
    /// </summary>
    private void OnApplicationQuit() => SaveGame();
}
