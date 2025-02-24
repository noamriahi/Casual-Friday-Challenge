using Core.Popups;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Settings popup, use a data class to save it in the playerPrefs
/// </summary>
public class SettingsPopup : Popup
{
    [Header("Settings Popup")]
    [SerializeField] Toggle _musicToggle;
    [SerializeField] Toggle _sfxToggle;
    [SerializeField] TMP_Dropdown _difficultyDropDown;

    public const string SETTINGS_DATA_KEY = "SettingsDataKey";
    public override void InitPopup()
    {
        var data = Utils.GetData<SettingsData>(SETTINGS_DATA_KEY);
        if (data != null)
        {
            _musicToggle.isOn = data.Music;
            _sfxToggle.isOn = data.Sfx;
            _difficultyDropDown.value = data.Difficulty;
        }
    }
    /// <summary>
    /// When closing the popup, it's save the last changes
    /// </summary>
    protected override void ClosePopup()
    {
        var data = new SettingsData()
        {
            Music = _musicToggle.isOn,
            Sfx = _sfxToggle.isOn,
            Difficulty = _difficultyDropDown.value
        };
        data.SaveData(SETTINGS_DATA_KEY);
        base.ClosePopup();
    }
}

[Serializable] 
public class SettingsData
{
    public bool Music;
    public bool Sfx;
    public int Difficulty;
}