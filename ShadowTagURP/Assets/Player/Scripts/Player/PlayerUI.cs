using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour, IDataPersistance
{
    public static PlayerUI Instance { get; private set; }
    private int currentPoints;          //The current points in this Labyrinth
    private int[] points;               //The Points from all levels
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
    public void UpdateScore(int newScoreText)
    {
        currentPoints += newScoreText;              //The new score is added to current score
        scoreText.text = "Score: "+ currentPoints;  //Update the text
    }
    public void UpdateAmmunition(int currentAmmunition,int MaxAmmunition) => ammunitionText.text = currentAmmunition + " / " + MaxAmmunition;    //Update the text
    public void UpdateText(string promptMessage) => promptText.text = promptMessage;  //Displays the message on the Screen
          
    public void UpdateLevel()
    {
        this.currentLabyrinthLevel = SaveManager.Instance.gameData.currentLabyrinthLevel;
        //Debug.Log(this.currentLabyrinthLevel);
        if (currentLabyrinthLevel == 0)
            currentLabyrinthText.text = "";
        else
            currentLabyrinthText.text = "Ebene: "+ this.currentLabyrinthLevel; //Update the text
    }
    //Save and Load
    public void SaveData(ref SaveData data)
    {
        data.currentPoints = this.currentPoints;
        data.points = this.points;
        data.currentLabyrinthLevel = this.currentLabyrinthLevel;
    }
    public void LoadData(SaveData data)
    {
        UpdateScore(data.currentPoints);
    }
    private void OnDisable() => SaveManager.Instance.dataPersistenceObjects.Remove(this);
}

