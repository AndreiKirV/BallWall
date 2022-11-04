using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager
{
    private Dictionary<string, GameObject> _gameObjects = new Dictionary<string, GameObject>();

    public static string Ball = "Ball";
    public static string Obstacle = "Obstacle";
    public static string FlatObstacle = "FlatObstacle";

    private void AddPrefab(string targetName)
    {
        GameObject tempObject = Resources.Load<GameObject>($"Prefabs/{targetName}");
        _gameObjects.Add(targetName, tempObject);
    }

    public void Init()
    {
        AddPrefab(Ball);
        AddPrefab(Obstacle);
        AddPrefab(FlatObstacle);
    }

    public GameObject GivePrefab(string target)
    {
        if (_gameObjects.ContainsKey(target))
        return _gameObjects[target];
        else
        return null;
    }
}