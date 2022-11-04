using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody _body;

    public UnityEvent Clash = new UnityEvent();

    private void Awake() 
    {
        _body = GetComponent<Rigidbody>();
    }

    private void Die()
    {
        if (Clash != null)
        Clash.Invoke();
        _body.useGravity = true;
    }

    private void OnCollisionEnter(Collision other) 
    {
        Die();
    }
}