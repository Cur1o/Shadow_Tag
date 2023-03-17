using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
//This scripts was part of a tutorial and the learned things 
public class FileSaveDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";
    public FileSaveDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public SaveData Load()
    {
        //Debug.Log("Loading SaveData");
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
                loadedData = JsonUtility.FromJson<SaveData>(dataToLoad);
                Debug.Log("Sucess Load SaveData");
            }
            catch (Exception e)
            {
                Debug.Log("Error while loading: " + fullPath + "\n" + e);  
            }
            
        }
        return loadedData;
    }
    public void Save(SaveData data)
    {
        Debug.Log("Saving SaveData");
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //Serialize C# game data to json
            string dataToStore = JsonUtility.ToJson(data, true);
            //Write file 
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            Debug.Log("sucess Saving SaveData");
        }
        catch (Exception e)
        {
            Debug.Log("Error while saving: "+ fullPath + "\n" + e);
        }
        
    }
}
