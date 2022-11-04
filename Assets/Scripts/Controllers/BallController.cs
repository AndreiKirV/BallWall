using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallController
{
    private Ball _ball;
    private GameObject _controllerObjective;
    private float _speed;
    private float _defaultSpeed = 40;
    private float _verticalVelocity = 5;
    private float _timer = 0;
    private float _previousSpeedIncrease = 0;
    private float _speedIncreaseStep = 15;
    private float _step = 5;

    private bool _isFalls = true;
    private bool _isDead = false;

    public UnityEvent Dead = new UnityEvent();

    private void Move()
    {
        _controllerObjective.transform.position = Vector3.Lerp(_controllerObjective.transform.position, new Vector3(_controllerObjective.transform.position.x, _controllerObjective.transform.position.y, _controllerObjective.transform.position.z + _speed), Time.deltaTime);
        
        if (_isFalls)
        _controllerObjective.transform.position = Vector3.Lerp(_controllerObjective.transform.position, new Vector3(_controllerObjective.transform.position.x, _controllerObjective.transform.position.y - _verticalVelocity, _controllerObjective.transform.position.z), Time.deltaTime);
        else if(!_isFalls)
        _controllerObjective.transform.position = Vector3.Lerp(_controllerObjective.transform.position, new Vector3(_controllerObjective.transform.position.x, _controllerObjective.transform.position.y + _verticalVelocity, _controllerObjective.transform.position.z), Time.deltaTime);
    }

    private void GainSpeed()
    {
        _verticalVelocity += _step;
    }

    private void Die()
    {
        _isDead = true;

        if (Dead!=null)
        Dead.Invoke();
    }

    public void Start()
    {
        _controllerObjective = Object.Instantiate<GameObject>(_controllerObjective);
        _ball = _controllerObjective.AddComponent<Ball>();
        _ball.Clash.AddListener(Die);
        TryCheckComplexityKey();
    }

    public void Update() 
    {
        if (!_isDead)
        Move();

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Mouse0))
        _isFalls = false;
        else
        _isFalls = true;

        _timer += Time.deltaTime;

        if (_previousSpeedIncrease + _speedIncreaseStep <= _timer)
        {
            _previousSpeedIncrease = _timer;
            GainSpeed();
        }
    }

    public void TryCheckComplexityKey()
    {
        if (PlayerPrefs.HasKey(UIController.ComplexityKey))
            _speed = _defaultSpeed * PlayerPrefs.GetInt(UIController.ComplexityKey);
        else
            _speed = _defaultSpeed;
    }

    public void SetBall(GameObject targetObject)
    {
        _controllerObjective = targetObject;
    }

    public GameObject GiveOBject()
    {
        if(_controllerObjective != null)
        return _controllerObjective;
        else
        {
        Debug.Log("Не назначена цель для камеры");
        return null;
        }
    }

    public float GiveTimer()
    {
        return _timer;
    }
}