//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

/// <summary>
/// Saves and loads config related data.
/// </summary>
public static class SaveManager {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const string GAME_STATE_FILENAME = "GameState.gd";
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    public static List<GameState> savedGameStates = new List<GameState>();
    
    public static void Save()
    {
        savedGameStates.Add(GameState.Current);
        BinaryFormatter bf = new BinaryFormatter();
       
        FileStream file = File.Create(Application.persistentDataPath + "/" + GAME_STATE_FILENAME); 
        bf.Serialize(file, savedGameStates);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/" + GAME_STATE_FILENAME))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + GAME_STATE_FILENAME, FileMode.Open);
            savedGameStates = (List<GameState>)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void SaveInt(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
    }

    public static int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static void SaveFloat(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
    }

    public static float GetFloat(string key, float defaultValue = 0f)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public static void SaveString(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
    }

    public static string GetString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
   
    #region Utility
    [MenuItem("Circinus/Data/Clear PlayerPrefs")]
    static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Circinus/Data/Remove Saved GameStates")]
    static void DeleteSavedFile()
    {
        File.Delete(Application.persistentDataPath + "/" + GAME_STATE_FILENAME);
    }
    #endregion
}

[System.Serializable]
public class GameState
{

    public static GameState Current;


    public GameState()
    {
        
    }
}