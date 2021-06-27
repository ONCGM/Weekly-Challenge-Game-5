using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Winch : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private string boxTag = "Box";
    [SerializeField, Range(0.2f, 2f)] private float reconnectionDelay = 0.7f;
    [SerializeField] private Vector3 jointConnectionOffset = Vector3.zero;
    [SerializeField] private Vector3 jointAnchor = Vector3.zero;

    [Header("Line Renderer Settings")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Vector3 playerOffset = Vector3.zero;
    [SerializeField] private List<Transform> points = new List<Transform>();

    private float nextTimeToConnect;
    private bool hasBox;
    private GameObject box;
    private FixedJoint joint;

    // Input.
    private void Update() {
        if(Input.GetKey(KeyCode.Space)) DropBox();

        for(var index = 0; index < points.Count; index++) {
            var point = points[index];
            
            lineRenderer.SetPosition(index, index > 0 ? point.position : point.position + playerOffset);
        }
    }

    // Grab a box.
    private void GrabBox(GameObject grabbedBox) {
        hasBox = true;
        box = grabbedBox;
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = grabbedBox.GetComponent<Rigidbody>();
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = jointAnchor;
        joint.connectedAnchor = jointConnectionOffset;
    }

    // Drop a box.
    private void DropBox() {
        if(!hasBox) return;

        nextTimeToConnect = Time.time + reconnectionDelay;
        Destroy(joint);
        hasBox = false;
        box = null;
    }

    // Collision detection.
    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag(boxTag) || hasBox || Time.time < nextTimeToConnect) return;
        
        GrabBox(other.gameObject);
    }
}
