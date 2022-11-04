using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController
{
    private Camera _camera;
    private float _drivingDistance = 15;
    private GameObject _targetObject;

    public void Update()
    {
        if (_targetObject.transform.position.z - _drivingDistance > _camera.transform.position.z)
        {
            _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, _targetObject.transform.position.z - _drivingDistance);
        }
    }

    public void InitCamera(Camera camera)
    {
        _camera = camera;
    }

    public void InitTarget(GameObject targetObject)
    {
        _targetObject = targetObject;
    }
}