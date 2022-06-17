using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InformationSelection 
{
    [SerializeField] private Collider2D _mainCollider;
    [SerializeField] private bool _moveble;

    public bool moveble { get => _moveble; }

    public float GetOffset() 
    { 
        return Vector3.Distance(_mainCollider.bounds.max, _mainCollider.bounds.center );
    }
    
}
