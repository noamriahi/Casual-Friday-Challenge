using System;
using UnityEngine;

public static partial class Utils
{
    public static void SaveData<T>(this T data, string dataKey)
    {
        try
        {
            if (data is int intValue)
            {
                PlayerPrefs.SetInt(dataKey, intValue);
            }
            else if (data is float floatValue)
            {
                PlayerPrefs.SetFloat(dataKey, floatValue);
            }
            else if (data is bool boolValue)
            {
                PlayerPrefs.SetInt(dataKey, boolValue ? 1 : 0); // Convert bool to int (1 = true, 0 = false)
            }
            else
            {
                string dataJson = JsonUtility.ToJson(data);
                PlayerPrefs.SetString(dataKey, dataJson);
            }

            PlayerPrefs.Save();
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception while saving data with key '{dataKey}': {e}");
        }
    }

    public static T GetData<T>(string key)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            return default;
        }

        try
        {
            if (typeof(T) == typeof(int))
            {
                return (T)(object)PlayerPrefs.GetInt(key);
            }
            if (typeof(T) == typeof(float))
            {
                return (T)(object)PlayerPrefs.GetFloat(key);
            }
            if (typeof(T) == typeof(bool))
            {
                return (T)(object)(PlayerPrefs.GetInt(key) == 1); // Convert int back to bool
            }

            string jsonData = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(jsonData);
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception while loading data with key '{key}': {e}");
            return default;
        }
    }
    public static string ToTimerFormat(this float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        return $"{minutes:00}:{seconds:00}"; // Formats as MM:SS
    }

}
