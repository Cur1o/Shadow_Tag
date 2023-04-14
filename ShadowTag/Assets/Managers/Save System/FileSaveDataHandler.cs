using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
//The Encryption in this part was from a totorial https://www.youtube.com/watch?v=gMfijpTkKX8
public class FileSaveDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";

    private static readonly string keyWord = "662138";
    public FileSaveDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    /// <summary>
    /// Loads saved data from a file and returns it as a SaveData object.
    /// </summary>
    /// <returns>The loaded SaveData object, or null if there is an error.</returns>
    public SaveData Load()
    {
        bool needDecryption = SaveManager.EncryptSaveFile;
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        SaveData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //Load the serialized Data from file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<SaveData>(needDecryption? EncryptDecrypt(dataToLoad) : dataToLoad);
                //Debug.Log("Sucess Load SaveData");
            }
            catch (Exception e)
            {
                Debug.Log("Error while loading: " + fullPath + "\n" + e);  
            }
        }
        return loadedData;
    }
    /// <summary>
    /// Serializes and saves the given SaveData object to a file.
    /// </summary>
    /// <param name="data">The SaveData object to be saved.</param>
    public void Save(SaveData data)
    {
        bool encryptData = SaveManager.EncryptSaveFile;
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //Serialize C# game data to json
            string dataToStore = JsonUtility.ToJson(data, true);
            if (encryptData)
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    var encryptedDataToStore = EncryptDecrypt(dataToStore);
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(encryptedDataToStore);
                    }
                }
            }
            else
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            //Debug.Log("sucess Saving SaveData");
        }
        catch (Exception e)
        {
            Debug.Log("Error while saving: "+ fullPath + "\n" + e);
        }
        
    }
    //Settings Part ------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Loads saved settings from a file and returns them as a SaveSettings object.
    /// </summary>
    /// <returns>The loaded SaveSettings object, or null if there is an error.</returns>
    public SaveSettings LoadSettings()
    {
        bool needDecryption = SaveManager.EncryptSaveFile;
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        SaveSettings loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //Load the serialized Data from file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<SaveSettings>(needDecryption ? EncryptDecrypt(dataToLoad) : dataToLoad);
                //Debug.Log("Sucess Load SaveSettings");
            }
            catch (Exception e)
            {
                Debug.Log("Error while loading: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    ///<summary>
    /// Saves the provided SaveSettings data to a file on disk.
    ///</summary>
    ///<param name="data">The SaveSettings data to be saved.</param>
    public void SaveSettings(SaveSettings data)
    {
        bool encryptData = SaveManager.EncryptSaveFile;
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //Serialize C# game data to json
            string dataToStore = JsonUtility.ToJson(data, true);
            if (encryptData)
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    var encryptedDataToStore = EncryptDecrypt(dataToStore);
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(encryptedDataToStore);
                    }
                }
            }
            else
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            //Debug.Log("sucess Saving SettingsData");
        }
        catch (Exception e)
        {
            Debug.Log("Error while saving: " + fullPath + "\n" + e);
        }
    }
    //This folowing part is from Youtube https://www.youtube.com/watch?v=gMfijpTkKX8
    /// <summary>
    /// Encrypts or decrypts a string using a simple XOR algorithm with a fixed keyword.
    /// </summary>
    /// <param name="data">The string to be encrypted/decrypted.</param>
    /// <returns>The encrypted/decrypted string.</returns>
    private static string EncryptDecrypt(string data)
    {
        string result = "";
        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ keyWord[i % keyWord.Length]);
        }
        return result;
    }
}
