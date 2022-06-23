using UnityEngine;

namespace _Project._ScriptsMain.Unit.MovebleUnit
{
    public abstract class MovableUnit : Unit
    {
        [SerializeField] protected UnitMovement _unitMovement;
        
        public abstract void Interact(IInteractable interactable);
        
        public abstract void MoveTo(Vector2 position);

        public abstract void StopMove();
    }
}