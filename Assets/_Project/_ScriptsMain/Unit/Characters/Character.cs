using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Character : Unit 
{
    [SerializeField] protected float _damage;
    [SerializeField] protected CharacterMovment _characterMovment;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] private InformationSelection _selectionInformation;

    protected Vector2 _lastDirection;

    public Direction GetDirection() => _characterMovment.GetDirection();

    public SpriteRenderer GetSpriteRenderer() => _spriteRenderer;

    public abstract void MoveBy(IInteractable interactable);

    public abstract void StopMove();

    public void GoTo(Vector2 position)
    {
        _characterMovment.GoTo(position);
    }
}
