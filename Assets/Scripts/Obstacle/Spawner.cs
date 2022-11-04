using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    private float _spawnLength;
    private float _step = 50;
    private float _minY = -4;
    private float _maxY = 12;
    private List<GameObject> _targetObjects = new List<GameObject>();
    private List <GameObject> _finishedObstacles = new List<GameObject>();

    public void SetSpawnLength(float length)
    {
        _spawnLength = length;
    }

    public void SetTargetPrefab(params GameObject[] objects)
    {
        foreach (var item in objects)
        {
            _targetObjects.Add(item);
        }
    }

    public void Spawn(Vector3 startPosition)
    {
        DestroyPrefabs();

        for (float currentStep = 0; currentStep < _spawnLength; currentStep += _step)
        {
            _finishedObstacles.Add(Object.Instantiate<GameObject>(_targetObjects[Random.Range(0, _targetObjects.Count)], new Vector3(startPosition.x, Random.Range(_minY, _maxY), startPosition.z + currentStep), Quaternion.Euler(0, 0, 90)));
        }
    }

    public void DestroyPrefabs()
    {
        if (_finishedObstacles.Count > _spawnLength / _step)
        {
            for (int i = 0; i < _finishedObstacles.Count - (_spawnLength / _step); i++)
            {
                Object.Destroy(_finishedObstacles[i]);
            }
        }
    }
}