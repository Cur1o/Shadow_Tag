using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SaveData 
{
    public int currentPoints;           //The points of the current level
    public int[] points;                //The Points from all levels
    public int currentLabyrinthLevel;   //The current Labyrinth level
    public Vector3 playerPosition;      //The current player Position
    
    //public List<GameObject> unlockedWeapons;       //To save all the weapons that are Unlocked
    //public GameObject currentActiveWeapon;         //Saves the current active weapon
    public SaveData()   //The variables are set to this numbers at a new game
    {
        
        this.currentPoints = 0;
        this.points = new int[] {0,0,0,0,0,0,0,0,0,0};
        this.currentLabyrinthLevel = 1;
        this.playerPosition = new Vector3(); 
        //this.unlockedWeapons = new() { };
        //this.currentActiveWeapon = null;
       
    }
}
