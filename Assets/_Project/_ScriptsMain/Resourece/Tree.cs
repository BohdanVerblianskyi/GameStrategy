using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IDescription, IDestroyable, IInteractable
{
    [SerializeField] private List<Transform> _interacteblePoints;
    [SerializeField] private string _name;
    [SerializeField] private int _healthPosint;
    [SerializeField] private int _minRecursesDrop;
    [SerializeField] private int _maxRecursesDrop;
    [SerializeField] private Transform _stump;

    private float _currentHealthPoint;
    
    private void Start()
    {
        _stump.gameObject.SetActive(false);
        _currentHealthPoint = _healthPosint;
    }

    public string GetName() => _name;

    

    public void SetDamage(float damage)
    {
        _currentHealthPoint -= damage;
        
        if (_currentHealthPoint<=0)
        {
            _stump.gameObject.SetActive(true);
            _stump.parent = null;
            Destroy(gameObject);
            RecursesDrop();
        }
    }

    private void RecursesDrop()
    {
        Debug.Log($"Drop > {Random.Range(_maxRecursesDrop, _maxRecursesDrop)}");
    }

    public List<Vector2> GetInteracteblePositions()
    {
        return _interacteblePoints.GetPositionList();
    }

    public Vector2 GetPosition() => transform.position;
}
