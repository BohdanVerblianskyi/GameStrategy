using UnityEngine;
using System.Collections.Generic;
using System;

public class Worker : Character
{
    private IInteractable _interactable;

    public override void MoveBy(IInteractable interactable)
    {
        _interactable = interactable;

        List<Vector2> interacteblepositions = _interactable.GetInteracteblePositions();
        Vector2 movePosition = interacteblepositions.GetClosestPosition(transform.position);

        GoTo(movePosition);
    }

    public override void SetDamage(float damage)
    {
    }

    public override void StopMove()
    {
        GoTo(transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(1);

        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            if (interactable == _interactable)
            {
                
            }
        }
    }
}
