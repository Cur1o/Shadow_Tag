using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistanceSettings 
{
    /// <summary>
    /// Loads data into the game from a SaveSettings object.
    /// </summary>
    /// <param name="data">The SaveSettings object containing the settings to be loaded.</param>
    /// <returns>None</returns>
    void LoadSettings(SaveSettings settings);
    /// <summary>
    /// Saves data from the game from a SaveSettings object.
    /// </summary>
    /// <param name="data">The SaveSettings object containing the settings to be saved.</param>
    /// <returns>None</returns>
    void SaveSettings(ref SaveSettings settings);
}
