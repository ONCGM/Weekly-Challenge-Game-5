using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTruck : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private string boxTag = "Box";
    [SerializeField] private int sceneToLoad;
    [SerializeField] private string sceneTime;


    /// <summary>
    /// Saves scored time if any better than last recorded.
    /// </summary>
    private void SaveScore() {
        var score = Time.timeSinceLevelLoad;
        
        if(score < PlayerPrefs.GetFloat(sceneTime)) return;
        
        PlayerPrefs.SetFloat(sceneTime, score);
        PlayerPrefs.Save();
    }
    
    // Collision Detection.
    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag(boxTag)) return;
        
        SaveScore();
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
