using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnAround : MonoBehaviour
{

    [SerializeField] private float speed;
    private float _angle;

    [SerializeField] private float sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating => Input.GetButton("Fire1");

    void Update() {
        if (_isRotating) RotateByMouse();
        else RotateAround();
    }

    void RotateAround() {
        _angle += Time.deltaTime * speed;
        Quaternion rotation = Quaternion.Euler(0,_angle,0);
        transform.rotation = rotation;
    }

    void RotateByMouse() {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        float xAxisRotation = Input.GetAxis("Mouse X") * sensitivity;
        float yAxisRotation = Input.GetAxis("Mouse Y") * sensitivity;
        
        transform.Rotate(Vector3.down, xAxisRotation);
        transform.Rotate(Vector3.right, yAxisRotation);
    }
}
