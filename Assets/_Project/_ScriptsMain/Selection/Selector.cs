using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] private BoxSelection _selectionBox;
    [SerializeField] private LayerMask _selectebleLayer;
    [SerializeField] private SelectionMark _selectionMarkPerfab;
    [SerializeField] private Rasa _rasa;

    private List<ISelectable> _currentSelectads;
    private Camera _camera;
    private Vector2 _startMousePosition;
    private Vector2 _endPosition;
    private Vector2 _center;
    private Vector2 _size;
    private const float DISTANCE_RAY = 100f;
    private const float MIN_PERCENTAGE_MOUSE_OFFSET = 0.01f;
    private bool _isMouseOffset;
    private float _minOffsetMouse;
    //private LayerMask _layerPlayer;

    private void Start()
    {
        _camera = Camera.main;

        Vector2 scrineSise = new Vector2(_camera.pixelHeight, _camera.pixelWidth);
        _minOffsetMouse = Vector2.Distance(Vector2.zero, scrineSise) * MIN_PERCENTAGE_MOUSE_OFFSET;

        _selectionBox.gameObject.SetActive(false);
        _currentSelectads = new List<ISelectable>();
        _isMouseOffset = false;
    }

    public bool TryGetSelectads<T>(out List<T> selectads) where T : ISelectable
    {
        selectads = new List<T>();
        foreach (ISelectable selectad in _currentSelectads)
        {
            if (selectad is T t)
            {
                selectads.Add(t);
            }
        }

        if (selectads.Count == 0)
        {
            return false;
        }

        return true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePosition = Input.mousePosition;
            CheckRaycast();
        }
        else if (Input.GetMouseButton(0))
        {
            CheckOffsetMouse();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isMouseOffset = false;
        }
    }

    private IEnumerator ShowSelectebleBox(Vector2 startPosition)
    {
        _selectionBox.gameObject.SetActive(true);
        while (Input.GetMouseButton(0))
        {
            _endPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _center = (startPosition + _endPosition) / 2;
            _size = new Vector2(Mathf.Abs(startPosition.x - _endPosition.x), Mathf.Abs(startPosition.y - _endPosition.y));

            _selectionBox.SetPosition(_center);
            _selectionBox.SetScale(_size);
            yield return null;
        }

        if (_selectionBox.TryGetSelecteds(out List<ISelectable> selecteds))
        {
            SelectionAll(selecteds);
        }

        _selectionBox.gameObject.SetActive(false);
    }

    private void CheckOffsetMouse()
    {
        if (_isMouseOffset)
        {
            return;
        }
        if (Vector2.Distance(_startMousePosition, Input.mousePosition) > _minOffsetMouse)
        {
            _isMouseOffset = true;
            Vector2 startPosition = _camera.ScreenToWorldPoint(_startMousePosition);
            StartCoroutine(ShowSelectebleBox(startPosition));
        }
    }

    private void CheckRaycast()
    {
        DeselectionAll();

        if (new RaycastHit().TryGetComoponentInMouseReycast( out ISelectable selectable,_selectebleLayer))
        {
            Selection(selectable);
        }
    }

    private void SelectionAll(List<ISelectable> selectables)
    {
        DeselectionAll();
        foreach (ISelectable selectable in selectables)
        {
            Selection(selectable);
        }
    }

    private void Selection(ISelectable selectable)
    {
        if (selectable.GetRasa() != _rasa)
        {
            return;
        }

        selectable.Select();
        _currentSelectads.Add(selectable);
    }

    private void DeselectionAll()
    {
        foreach (ISelectable selectad in _currentSelectads)
        {
            selectad.Deselect();
        }
        _currentSelectads.Clear();
    }

    private RaycastHit2D GetRaycast(LayerMask layerMask)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        return Physics2D.GetRayIntersection(ray, DISTANCE_RAY, layerMask);
    }

    private bool TryGetRaycast(LayerMask layerMask,out RaycastHit raycastHit)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
