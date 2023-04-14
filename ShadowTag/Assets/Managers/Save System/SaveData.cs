using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SaveData 
{
    [Header("Game Data")]
    public int currentPoints;           //The points of the current level
    public int currentLabyrinthLevel;   //The current Labyrinth level
    public Vector3 playerPosition;      //The current player Position
    public List<bool> unlockedWeapons;  //To save all the weapons that are Unlocked
    public List<bool> unlockedLevels;   //Unlocked Levels
    /// <summary>
    /// Te default Values for the Variables
    /// </summary>
    public SaveData()   //The variables are set to this numbers at a new game
    {
        this.currentPoints = 0;
        this.currentLabyrinthLevel = 0;
        this.playerPosition = Vector3.zero;  
        this.unlockedWeapons = new List<bool> {false,false,false,false};
        this.unlockedLevels = new List<bool> { true, false, false, false };
    }
}
