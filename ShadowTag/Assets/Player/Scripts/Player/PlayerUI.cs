using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; private set; }
    private int currentScore;
    [SerializeField] private TextMeshProUGUI promptText;    //Creates a TMPro text element
    [SerializeField] private TextMeshProUGUI scoreText;     //Shows the current score
    [SerializeField] private TextMeshProUGUI ammunitionText;//Shows the current ammunition on screen
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
    public void UpdateScore(int newScoreText)
    {
        currentScore += newScoreText;
        scoreText.text = "Score: "+currentScore; 
    }
    public void UpdateAmmunition(int currentAmmunition,int MaxAmmunition)
    {
        ammunitionText.text = currentAmmunition + " / " + MaxAmmunition;
    }
    public void UpdateText(string promptMessage)
    {
            promptText.text = promptMessage;    //Displays the message on the Screen
    }
}

