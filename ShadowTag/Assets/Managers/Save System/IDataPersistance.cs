using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistance 
{
    /// <summary>
    /// Loads data into the game from a SaveData object.
    /// </summary>
    /// <param name="data">The SaveData object containing the data to be loaded.</param>
    /// <returns>None</returns>
    void LoadData(SaveData data);
    /// <summary>
    /// Saves data from the game from a SaveData object.
    /// </summary>
    /// <param name="data">The SaveData object containing the data to be saved.</param>
    /// <returns>None</returns>
    void SaveData(ref SaveData data);
}
