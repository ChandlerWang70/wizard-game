using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code based on tutorial from Dave / GameDevelopment on YouTube
// https://www.youtube.com/watch?v=f473C43s8nE&t=128s
public class CameraMovement : MonoBehaviour
{

    public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}

