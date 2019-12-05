using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int horizontalMinBorder;
    [SerializeField] private int horizontalMaxBorder;
    [SerializeField] private int verticalMinBorder;
    [SerializeField] private int verticalMaxBorder;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float zoomSpeed;
    
    [SerializeField] private float hMax = 20f;
    private float _hNormal;   
    
    [SerializeField] private float rMin = 30f;
    private float _rNormal;
    
    private Camera _camera;

    private void Start()
    {
        _hNormal = transform.position.y;
        _rNormal = transform.rotation.x;
        
        _camera = Camera.main;
    }

    private void Update()
        {
            if (Input.GetKey(KeyCode.W) && transform.position.z <= verticalMaxBorder)
            {
                transform.position += Vector3.forward * movementSpeed;
            }
            
            if (Input.GetKey(KeyCode.S) && transform.position.z >= verticalMinBorder)
            {
                transform.position += Vector3.back * movementSpeed;
            }
            
            if (Input.GetKey(KeyCode.A) && transform.position.x >= horizontalMinBorder)
            {
                transform.position += Vector3.left * movementSpeed;
            }
            
            if (Input.GetKey(KeyCode.D) && transform.position.x <= horizontalMaxBorder)
            {
                transform.position += Vector3.right * movementSpeed;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
            {
                ZoomIn();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
            {
                ZoomOut();
            }
        }

    void ZoomIn()
    {
        if (_camera.fieldOfView > hMax)
        {
            transform.position = new Vector3(transform.position.x,Mathf.Lerp(transform.position.y, hMax, Time.deltaTime * zoomSpeed), transform.position.z);
            //lerp angle
        }
    }

    void ZoomOut()
    {
        if (_camera.fieldOfView < _hNormal)
        {
            transform.position = new Vector3(transform.position.x,Mathf.Lerp(transform.position.y, _hNormal, Time.deltaTime * zoomSpeed), transform.position.z);
            //lerp angle
        }
    }
}
