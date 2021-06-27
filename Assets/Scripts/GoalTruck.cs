using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTruck : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private string boxTag = "Box";
    [SerializeField] private int amountOfBoxes = 1;
    [SerializeField] private int sceneToLoad;
    [SerializeField] private string sceneTime;

    private int storedBoxes;


    /// <summary>
    /// Saves scored time if any better than last recorded.
    /// </summary>
    private void SaveScore() {
        var score = Time.timeSinceLevelLoad;
        
        if(score < PlayerPrefs.GetFloat(sceneTime)) return;
        
        PlayerPrefs.SetFloat(sceneTime, score);
        PlayerPrefs.Save();
    }

    
    /// <summary>
    /// Stores a box.
    /// </summary>
    private void StoreBox(GameObject box) {
        box.tag = "Finish";
        storedBoxes++;
        Destroy(box.GetComponent<Rigidbody>(), 3f);
    }
    
    // Collision Detection.
    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag(boxTag)) return;

        StoreBox(other.gameObject);
        
        if(storedBoxes < amountOfBoxes) return;
        SaveScore();
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
