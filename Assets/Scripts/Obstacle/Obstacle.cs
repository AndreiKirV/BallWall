using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private List<GameObject> _parts = new List<GameObject>();

    private void OnCollisionEnter(Collision other) 
    {
        if (_parts.Count > 0)
        {
            for (int i = 0; i < _parts.Count; i++)
            {
                GameObject tempObject = _parts[i];
                tempObject.transform.parent = null;
                tempObject.SetActive(true);
                Object.Destroy(tempObject, 5);
            }
        }

        Object.Destroy(gameObject);
    }
}