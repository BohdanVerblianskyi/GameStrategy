using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using _Project._ScriptsMain.ExtractedResource;
using _Project._ScriptsMain.Unit;

public class Worker : Character
{
    private IInteractable _interactableTarget;
    private bool _isCollisionForInteractableTarget;


    public override void Interact(IInteractable interactable)
    {
        _interactableTarget = interactable;

        List<Vector2> interactablePositions = _interactableTarget.GetInteractablePositions();
        Vector2 movePosition = interactablePositions.GetClosestPosition(transform.position);

        _unitMovement.MoveTo(movePosition);
    }

    protected override void Start()
    {
        base.Start();

        _interactableTarget = null;
        _isCollisionForInteractableTarget = false;
        _unitMovement.EndMoveEvent += UnitMovementOnEndMoveEvent;
        _unitMovement.StartMoveEvent += UnitMovementOnStartMoveEvent;
    }

    private void UnitMovementOnStartMoveEvent()
    {
        Debug.Log("Start");
    }

    private void UnitMovementOnEndMoveEvent()
    {
        if (!IsCollisionForInteractableTarget())
        {
            return;
        }

        if (_interactableTarget is ExtractedResource extractedResource)
        {
            StartCoroutine(Attack(extractedResource));
        }
        else if (_interactableTarget is Building building)
        {
            Debug.Log(building.name);
        }
    }

    private bool IsCollisionForInteractableTarget()
    {
        TryGetAllCollisionInComponent(out List<IInteractable> interactableList, _attackRange);
        foreach (var interactable in interactableList)
        {
            if (interactable == _interactableTarget)
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator Attack(IDestroyable destroyable)
    {
        while (IsCollisionForInteractableTarget())
        {
            TryAttack(destroyable);
            yield return null;
        }
    }
    
    public override void MoveTo(Vector2 position)
    {
        _unitMovement.MoveTo(position);
        _interactableTarget = null;
    }

    public override void SetDamage(float damage)
    {
    }

    public override void StopMove()
    {
    }
}