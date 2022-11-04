using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CorridorsController
{
    private List <GameObject> _corridors = new List<GameObject>();
    
    private GameObject _targetObject;
    private float _corridorLength = 1000;
    private float _maximumDistanceToObject = 1010;

    public UnityEvent<Vector3> PositionHasChanged = new UnityEvent<Vector3>();

    public float CorridorLength => _corridorLength;

    public void Update() 
    {
        for (int i = 0; i < _corridors.Count; i++)
        {
            if (_targetObject.transform.position.z -  _maximumDistanceToObject > _corridors[i].transform.position.z)
            {
                _corridors[i].transform.position = new Vector3(_corridors[i].transform.position.x, _corridors[i].transform.position.y, _corridors[i].transform.position.z + _corridorLength *_corridors.Count);

                if (PositionHasChanged != null)
                {
                    PositionHasChanged.Invoke(_corridors[i].transform.position);
                }
            }
        }
    }

    public void SetCorridors(List<GameObject> corridors)
    {
        _corridors = corridors;
    }

    public void InitTarget(GameObject targetObject)
    {
        _targetObject = targetObject;
    }
}