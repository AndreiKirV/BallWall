using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private List <GameObject> _corridors = new List<GameObject>();
    [SerializeField] private UIController _uIController;
    private PrefabManager _objectController = new PrefabManager();
    private BallController _ball = new BallController();
    private CameraController _cameraController = new CameraController();
    private CorridorsController _corridorsController = new CorridorsController();
    private Spawner _spawner = new Spawner();

    private void Awake() 
    {
        _objectController.Init();

        _cameraController.InitCamera(_camera);
        _corridorsController.SetCorridors(_corridors);

        _spawner.SetSpawnLength(_corridorsController.CorridorLength);
        _spawner.SetTargetPrefab(_objectController.GivePrefab(PrefabManager.Obstacle), _objectController.GivePrefab(PrefabManager.FlatObstacle));

        foreach (var item in _corridors)
        {
            _spawner.Spawn(item.transform.position);
        }
    }

    private void Start() 
    {
        _ball.SetBall(_objectController.GivePrefab(PrefabManager.Ball));
        _ball.Start();
        _ball.Dead.AddListener(delegate {
            StartCoroutine(InvokeDelegateCor(_uIController.OpenGameOver, 1f));
            });

        _cameraController.InitTarget(_ball.GiveOBject());
        _corridorsController.InitTarget(_ball.GiveOBject());

        _corridorsController.PositionHasChanged.AddListener(_spawner.Spawn);

        _uIController.ComplexityHasChanged.AddListener(_ball.TryCheckComplexityKey);
        _uIController.TimeRequest += _ball.GiveTimer;
    }

    private void Update() 
    {
        _ball.Update();
        _cameraController.Update();
        _corridorsController.Update();
    }

    private IEnumerator InvokeDelegateCor(Action func, float time) 
    {
        yield return new WaitForSeconds(time);
        func();
    }
}