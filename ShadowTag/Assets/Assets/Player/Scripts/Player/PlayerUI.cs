using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour, IDataPersistance
{
    public static PlayerUI Instance { get; private set; }
    private int currentPoints;          //The current points in this Labyrinth
    private int[] points;               //The Points from all levels
    private int currentLabyrinthLevel;  //The current Labyrinth level
    [SerializeField] private TextMeshProUGUI promptText;    //Creates a TMPro text element
    [SerializeField] private TextMeshProUGUI scoreText;     //Shows the current score
    [SerializeField] private TextMeshProUGUI ammunitionText;//Shows the current ammunition on screen
    [SerializeField] private TextMeshProUGUI currentLabyrinthText; //Shows the current Labyrinth on screen
    // Start is called before the first frame update
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
        
    }
    private void Start()
    {
        SaveManager.Instance.dataPersistenceObjects.Add(this);
        LoadData(SaveManager.Instance.gameData);
    }
    public void UpdateScore(int newScoreText)
    {
        currentPoints += newScoreText;
        scoreText.text = "Score: "+ currentPoints; 
    }
    public void UpdateAmmunition(int currentAmmunition,int MaxAmmunition)
    {
        ammunitionText.text = currentAmmunition + " / " + MaxAmmunition;
    }
    public void UpdateText(string promptMessage)
    {
            promptText.text = promptMessage;    //Displays the message on the Screen
    }
    public void UpdateLevel(int newLevel)
    {
        currentLabyrinthLevel += newLevel;
        currentLabyrinthText.text = "Ebene: "+currentLabyrinthLevel;
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
        Debug.Log(data.currentPoints);
        UpdateScore(data.currentPoints);
        UpdateLevel(data.currentLabyrinthLevel);
    }
    private void OnDisable()
    {
        SaveManager.Instance.dataPersistenceObjects.Remove(this);
    }

}

