using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraMovement : MonoBehaviour
{
    //Camera Zoom
    public Camera mainCam;
    public float minZoom, maxZoom, zoomAmount;

    //Camera Movement
    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    public int screenMax;

    // Start is called before the first frame update
    void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera zoom out
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(mainCam.orthographicSize > minZoom)
            {
                mainCam.orthographicSize -= zoomAmount;
            }
        }
        //Camera zoom in
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (mainCam.orthographicSize < maxZoom)
            {
                mainCam.orthographicSize += zoomAmount;
            }
        }

        if(transform.position.x < ResetCamera.x - screenMax || transform.position.x > ResetCamera.x + screenMax || transform.position.y < ResetCamera.y - screenMax || transform.position.y > ResetCamera.y + screenMax)
            {
                Camera.main.transform.position = ResetCamera;
            }
        
        
    }

    void LateUpdate()
    {
        //Camera Movement
        if (Input.GetMouseButton(2))
        {
            Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            Camera.main.transform.position = Origin - Diference;
        }
        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = ResetCamera;
        }
    }

}

