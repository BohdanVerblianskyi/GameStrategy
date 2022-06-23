using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using _Project._ScriptsMain.Unit.MovebleUnit;
using _Project._ScriptsMain.Unit;

public class MovableUnitController
{
    private readonly Camera _camera;
    private readonly PlayingInput _playingInput;
    private List<MovableUnit> _movableUnits;
        
    public MovableUnitController(Selector selector, PlayingInput playingInput)
    {
        _playingInput = playingInput;
        _camera = Camera.main;
        _movableUnits = new List<MovableUnit>();

        _playingInput.Mouse.RightButton.performed += context => RightMouseButtonClick();

        selector.UpdateSelectedEvent += UpdateSelectionMovableUnits;
    }

    /*private void PositionMouseChang(InputAction.CallbackContext callbackContext)
    {
        CheckWhatTheMousePointerAt(callbackContext.ReadValue<Vector2>());
    }*/

    private void CheckWhatTheMousePointerAt(Vector2 mousePosition)
    {
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit);

        if (raycastHit.collider.TryGetComponent(out IInteractable interactable))
        {
                InteractableProcessing(interactable);
        }
        
    }

    private void InteractableProcessing(IInteractable interactable)
    {
        if (interactable is Unit unit)
        {
            Debug.Log(unit.GetRasa());
        }
        
    }
    
    private void UpdateSelectionMovableUnits(Selector selector)
    {
        selector.TryGetSelectedComponents(out _movableUnits);
    }
    
    private void RightMouseButtonClick()
    {
        if (_movableUnits.Count == 0)
        {
            return;
        }
        
        Vector2 mousePosition = _playingInput.Mouse.Position.ReadValue<Vector2>();
        
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        if (ray.TryGetComponentFromRaycast(out IInteractable interactable))
        {
            foreach (var movableUnit in _movableUnits)
            {
                movableUnit.Interact(interactable);
            }
            
            return;
        }

        Vector2 movePosition = mousePosition.ToWorldPosition(_camera);
        
        foreach (var movableUnit in _movableUnits)
        {
            movableUnit.MoveTo(movePosition);
        }
    }
}