using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private int gameSceneIndex = 1;
    
    [Header("Components")]
    [SerializeField] private TMP_Text bestTimeText;

    // Update text.
    private void Awake() {
        if(!bestTimeText) return;

        var timeOne = PlayerPrefs.GetFloat("Time1", 0);
        var timeTwo = PlayerPrefs.GetFloat("Time2", 0);
        var timeThree = PlayerPrefs.GetFloat("Time3", 0);
        
        bestTimeText.text = $"Best Time: {(timeOne + timeTwo + timeThree):F}s" +
                            $"\nFirst Scene: {(timeOne):F}s" +
                            $"\nSecond Scene: {(timeTwo):F}s" +
                            $"\nThird Scene: {(timeThree):F}s";
    }

    /// <summary>
    /// Loads the main game scene.
    /// </summary>
    public void LoadGameScene() {
        SceneManager.LoadSceneAsync(gameSceneIndex);
    }

    /// <summary>
    /// Toggle audio on or off.
    /// </summary>
    public void ToggleAudio() {
        var sound = PlayerPrefs.GetInt("Sound") > 0 ? 0 : 1;
        PlayerPrefs.SetInt("Sound", sound);
        PlayerPrefs.Save();
        
        FindObjectOfType<PlayerController>().UpdateSoundSettings();
    }
}
