using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public class CharacterMovment : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _navMeshAgent;

    public event Action onStartMoveEvent;
    public event Action onEndMoveEvent;
    private Vector2 _lastDirection;
    private State _state;
    private bool _moving;

    private void Start()
    {
        _moving = false;
        _state = State.Stand;
        _lastDirection = Vector2.down;

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        MovingCheck();
        ChangeState();
        Vector2 temp = transform.position - _navMeshAgent.nextPosition;

        Debug.Log(temp);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, _navMeshAgent.nextPosition);
    }

    private void MovingCheck()
    {
        if (Mathf.Abs(_navMeshAgent.velocity.x) + Mathf.Abs(_navMeshAgent.velocity.y) > 0f)
        {
            _moving = true;
            _lastDirection = _navMeshAgent.velocity;
        }
        else
        {
            _moving = false;
        }
    }

    public void Stop()
    {

    }

    private void ChangeState()
    {
        bool beginMoving = _moving && _state == State.Stand;
        bool endMoving = !_moving && _state == State.Move;

        if (beginMoving)
        {
            onStartMoveEvent?.Invoke();
            _state = State.Move;
        }
        else if (endMoving)
        {
            onEndMoveEvent?.Invoke();
            _state = State.Stand;
        }
    }

    public Vector2 GetVelocity() => _navMeshAgent.velocity;

    public Direction GetDirection() => _lastDirection.GetDirection();

    public void GoTo(Vector2 position)
    {
        _navMeshAgent.SetDestination(position);
    }

    private enum State
    {
        Stand,
        Move
    }
}
