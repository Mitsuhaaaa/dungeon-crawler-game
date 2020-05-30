using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Transform objectToFollow;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = objectToFollow.transform.position + cameraOffset; 
    }
}
