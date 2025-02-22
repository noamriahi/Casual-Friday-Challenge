using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void SaveData<T>(this T data, string dataKey)
    {
        try
        {
            string dataJson = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(dataKey, dataJson);
            PlayerPrefs.Save();
        }
        catch(Exception e)
        {
            Debug.LogError($"There is an exception while trying to save data with key: {dataKey}, exception => {e}");
        }
    }
    public static T GetData<T>(string key)
    {
        if(PlayerPrefs.HasKey(key))
        {
            string jsonData = PlayerPrefs.GetString(key);
            T dataObject = JsonUtility.FromJson<T>(jsonData);
            return dataObject;
        }
        return default;
    }
}
