using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Settings")]
    [SerializeField, Range(0.5f, 25f)] private float movementSpeed = 5f;
    [SerializeField, Range(0.05f, 2f)] private float rotationSpeed = 0.5f;

    private Vector2 movementDirection = Vector2.zero;
    private Vector3 rotation = Vector3.zero;
    
    // Components.
    private Rigidbody rb;
    private AudioSource aSource;

    // Setup.
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        aSource = GetComponent<AudioSource>();
        aSource.mute = PlayerPrefs.GetInt("Sound") > 0;
    }

    // Input.
    private void Update() {
        if(Input.GetKey(KeyCode.W)) {
            movementDirection += Vector2.up;
            aSource.pitch = 1.05f;
            aSource.volume = 0.4f;
        }

        if(Input.GetKey(KeyCode.S)) {
            movementDirection += Vector2.down;
            aSource.pitch = 0.95f;
            aSource.volume = 0.2f;
        }
        
        if(Input.GetKey(KeyCode.D)) {
            movementDirection += Vector2.right;
            rotation = new Vector3(0f, 0f, -45f);
        }
        
        if(Input.GetKey(KeyCode.A)) {
            movementDirection += Vector2.left;
            rotation = new Vector3(0f, 0f, 45f);
        }
        
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) return;
        aSource.pitch = 1f;
        aSource.volume = 0.3f;
    }

    // Movement w/ physics.
    private void FixedUpdate() {
        rb.AddForce(movementDirection * movementSpeed, ForceMode.Force);
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, Quaternion.Euler(rotation), rotationSpeed));
        movementDirection = Vector2.zero;
        rotation = Vector3.zero;
    }

    /// <summary>
    /// Updates the sound to match the player settings.
    /// </summary>
    public void UpdateSoundSettings() {
        aSource.mute = PlayerPrefs.GetInt("Sound") > 0;
    }
}
