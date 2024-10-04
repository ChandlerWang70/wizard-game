using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code based on tutorial from Dave / GameDevelopment on YouTube
// https://www.youtube.com/watch?v=f473C43s8nE&t=128s
public class WizardCamera : MonoBehaviour
{

    public float sensX;
    public float sensY;

    public Transform orientation;
    float rotationX;
    float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //locks curos to middle of screen
        Cursor.visible = false; //makes cursor invisible
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float moveY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationY += moveX; // rotation around y axis vs movement on x axis

        rotationX -= moveY; // rotation around x axis vs movement on y axis
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // mimics head movement limits

        // move camera
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);

    }
}
