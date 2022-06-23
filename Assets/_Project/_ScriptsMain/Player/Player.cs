using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private BoxSelectionVisual _boxSelectionVisualPrefab;
    [SerializeField] private LayerMask _selectionLayer;
    [SerializeField] private Rasa _rasa;

    private Selector _selector;
    private PlayingInput _playingInput;
    private Camera _camera;
    private MovableUnitController _movableUnitController;

    private void Start()
    {
        _playingInput = new PlayingInput();
        _playingInput.Enable();

        _camera = Camera.main;

        _selector = new Selector(_playingInput, _camera, _selectionLayer);
        _movableUnitController = new MovableUnitController(_selector, _playingInput);
        var box = Instantiate(_boxSelectionVisualPrefab, transform);
        box.Init(_playingInput,_camera);
    }
}