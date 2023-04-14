using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour, IDataPersistance
{
    public static PlayerUI Instance { get; private set; }
    private int currentPoints;          //The current points in this Labyrinth
    private int currentLabyrinthLevel = 0;  //The current Labyrinth level
    [SerializeField] private TextMeshProUGUI promptText;    //Creates a TMPro text element
    [SerializeField] private TextMeshProUGUI scoreText;     //Shows the current score
    [SerializeField] private TextMeshProUGUI ammunitionText;//Shows the current ammunition on screen
    [SerializeField] private TextMeshProUGUI currentLabyrinthText; //Shows the current Labyrinth on screen
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null || Instance == this) 
            Destroy(gameObject);   
        else 
            Instance = this;
    }
    private void Start()
    {   //When Ui is created or active, it adds itself to dataPersistenceObjects List in SaveManager.
        SaveManager.Instance.dataPersistenceObjects.Add(this);  //Using foreach to find every object with IDataPrersistance Interface is bad for performance
        LoadData(SaveManager.Instance.gameData);    //The Data is Loaded
    }
    /// <summary>
    /// Updates the current player Score on UI
    /// </summary>
    /// <param name="newScoreText"> The current Score Text displayed on screen </param>
    public void UpdateScore(int newScoreText)
    {
        currentPoints += newScoreText;              //The new score is added to current score
        scoreText.text = "Score: "+ currentPoints;  //Update the text
    }
    /// <summary>
    /// Updates the Ammmunition Count on screen
    /// </summary>
    /// <param name="currentAmmunition"> The current Ammunition count from the current Gun</param>
    /// <param name="MaxAmmunition"> The current Max Ammunition value from the current Gun</param>
    public void UpdateAmmunition(int currentAmmunition,int MaxAmmunition) => ammunitionText.text = currentAmmunition + " / " + MaxAmmunition;    //Update the text
    /// <summary>
    /// The Text in the moddle of the screen is shown tho guide the player.
    /// </summary>
    /// <param name="promptMessage"> Masage that is shown on screen to guide the player </param>
    public void UpdateText(string promptMessage) => promptText.text = promptMessage;  //Displays the message on the Screen
    /// <summary>
    /// Updates the Current currentLabyrinthLevel
    /// </summary>
    public void UpdateLevel()
    {
        this.currentLabyrinthLevel = SaveManager.Instance.gameData.currentLabyrinthLevel;
        if (currentLabyrinthLevel == 0)
            currentLabyrinthText.text = "";
        else
            currentLabyrinthText.text = "Ebene: "+ (this.currentLabyrinthLevel -1); //Update the text
    }
    //Save and Load
    /// <summary>
    /// Inherited from IDataPersistance Saves the currentPoints and currentLabyrinthLevel to the gameData.
    /// </summary>
    /// <param name="data">The currentPoints and currentLabyrinthLevel</param>
    public void SaveData(ref SaveData data)
    {
        data.currentPoints = this.currentPoints;
        data.currentLabyrinthLevel = this.currentLabyrinthLevel;
    }
    /// <summary>
    /// Inherited from IDataPersistance Loads the game data. And updates the UI
    /// </summary>
    /// <param name="data">The currentPoints </param>
    public void LoadData(SaveData data) => UpdateScore(data.currentPoints);
    private void OnDisable() => SaveManager.Instance.dataPersistenceObjects.Remove(this);
}

