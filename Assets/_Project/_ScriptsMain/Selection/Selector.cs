using System;
using System.Collections.Generic;
using System.Linq;
using _Project._ScriptsMain.Selection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Selector
{
    public event Action<Selector> UpdateSelectedEvent;

    private List<ISelectable> _currentSelected;
    private readonly BoxSelectionVisual _selectionVisualBox;
    private readonly PlayingInput _playingInput;
    private readonly Camera _camera;
    private BoxCastSelection _boxCastSelection;

    public Selector(PlayingInput playingInput, Camera camera, LayerMask selectionLayerMask)
    {
        _camera = camera;
        _currentSelected = new List<ISelectable>();
        _playingInput = playingInput;

        _playingInput.Mouse.LeftButton.started += MouseButtonLeftDown;
        _playingInput.Mouse.Position.performed += MouseChangePosition;

        _boxCastSelection = new BoxCastSelection(this, selectionLayerMask);
    }

    private void MouseButtonLeftDown(InputAction.CallbackContext callbackContext)
    {
        _boxCastSelection.Start(MousePosition().ToWorldPosition(_camera));
        ChangeCurrentSelected(_boxCastSelection.GetSelected());
    }

    private void MouseChangePosition(InputAction.CallbackContext callbackContext)
    {
        if (_playingInput.Mouse.LeftButton.IsPressed())
        {
            _boxCastSelection.Tick(MousePosition().ToWorldPosition(_camera));
            ChangeCurrentSelected(_boxCastSelection.GetSelected());
        }
    }

    public bool TryGetSelectedComponents<T>(out List<T> components) where T : ISelectable
    {
        components = new List<T>();
        foreach (ISelectable selected in _currentSelected)
        {
            if (selected is T component)
            {
                components.Add(component);
            }
        }

        if (components.Count == 0)
        {
            return false;
        }

        return true;
    }

    private Vector2 MousePosition() => _playingInput.Mouse.Position.ReadValue<Vector2>();

    private void ChangeCurrentSelected(List<ISelectable> selectables)
    {
        if (selectables == _currentSelected)
        {
            return;
        }

        _currentSelected = selectables;
        UpdateSelectedEvent?.Invoke(this);
    }
}