using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SaveData 
{
    public int currentPoints;           //The points of the current level
    //public int[] points;                //The Points from all levels
    public int currentLabyrinthLevel;   //The current Labyrinth level
    public Vector3 playerPosition;      //The current player Position
    public List<bool> unlockedWeapons;  //To save all the weapons that are Unlocked
    public List<bool> unlockedLevels;   //Unlocked Levels   
    public SaveData()   //The variables are set to this numbers at a new game
    {
        this.currentPoints = 0;
        //this.points = new int[] {0,0,0,0,0};
        this.currentLabyrinthLevel = 0;
        this.playerPosition = Vector3.zero;  
        this.unlockedWeapons = new List<bool> {false,false,false,false};
        this.unlockedLevels = new List<bool> { true, false, false, false };
    }
}
