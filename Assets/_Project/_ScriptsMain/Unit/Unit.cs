using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDescription, IDestroyable, ISelectable,IInteractable
{
    [SerializeField] private List<Transform> _interacteblePoints;
    [SerializeField] private Rasa _rasa;
    [SerializeField] private string _name;
    [SerializeField] private float _heatltPoint;
    [SerializeField] private SelectionMark _selectionMark;

    protected virtual void Start()
    {
        _selectionMark.gameObject.SetActive(false);
    }

    public Vector2 GetPosition() => transform.position;

    public Rasa GetRasa() => _rasa;

    public string GetName() => _name;

    public abstract void SetDamage(float damage);

    public void Select()
    {
        _selectionMark.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        _selectionMark.gameObject.SetActive(false);
    }

    public List<Vector2> GetInteracteblePositions()
    {
        return _interacteblePoints.GetPositionList();
    }
}

public enum Rasa
{
    Red,
    Blue
}