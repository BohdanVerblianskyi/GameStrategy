using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopupWindow : MonoBehaviour
{
    [SerializeField] private Transform _activePoint;
    [SerializeField] private Transform _deactivePoint;
    [SerializeField] private float _speed;
    [SerializeField] private Button _button;

    public event Action OnEndMove;
    private Vector3 _activePosition;
    private Vector3 _deactivePosition;
    private Vector3 _endPosition;
    private IEnumerator _moveCoroutine;
    private float _dostans;
    private bool _active;



    public Button Button { get => _button;  }
    public bool Active { get => _active; }

    private void Start()
    {
        _activePosition = _activePoint.position;
        _deactivePosition = _deactivePoint.position;
        transform.position = _deactivePosition;
        _dostans = Vector3.Distance(_activePosition, _deactivePosition);
        _active = false;
    }

    public void ChangeWindowStat()
    {
        _active = !_active;

        if (_active)
        {
            _endPosition = _activePosition;
        }
        else
        {
            _endPosition = _deactivePosition;
        }

        if (_moveCoroutine != null)
        {
            return;
        }

        _moveCoroutine = Move();
        StartCoroutine(_moveCoroutine);
    }

    private IEnumerator Move()
    {
        while (transform.position != _endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, Time.deltaTime * _dostans *_speed);
            yield return null;
        }
        _moveCoroutine = null;
        OnEndMove?.Invoke();
    }
}
