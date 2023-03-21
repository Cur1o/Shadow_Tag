using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensitivity;
    public float ySensitivity;
    public int cFOV; 
    private void Start()
    {
        cFOV = GameManager.Instance.cFOV;
        //Debug.Log(cFOV);
        cam.fieldOfView = cFOV;
    }
    public void ManageLook(Vector2 input)
    {
        if (input.x < 1 && input.x > -1) xSensitivity = GameManager.Instance.xCSliderValue; else xSensitivity = GameManager.Instance.xSliderValue;
        if (input.y < 1 && input.y > -1) ySensitivity = GameManager.Instance.yCSliderValue; else ySensitivity = GameManager.Instance.ySliderValue;
        Cursor.lockState = CursorLockMode.Locked;
        float mouseX = input.x; //Mouse x input
        float mouseY = input.y; //Mouse Y input
        //Y Rotation
        xRotation -= (mouseY * Time.deltaTime * ySensitivity);  //Adds the mouse movmet to the rotation of the camera
        xRotation = Mathf.Clamp(xRotation, -90, 90);            //the y rotation is limited to -90 down and +90 up 
        //Apply to camera
        cam.transform.localRotation = Quaternion.Euler(xRotation , 0 , 0);  //The Y rotation is applyed to the Camera
        //Rotate the Player
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
