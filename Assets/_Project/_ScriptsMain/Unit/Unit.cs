using System.Collections;
using System.Collections.Generic;
using _Project._ScriptsMain.Unit;
using UnityEngine;

namespace _Project._ScriptsMain.Unit
{
    public abstract class Unit : MonoBehaviour, IDestroyable, ISelectable
    {
        [SerializeField] private UnitData _unitData;
        [SerializeField] private List<Transform> _interacteblePoints;
        [SerializeField] private SelectionMark _selectionMark;

        protected float _currentHealthPoint;
    
        protected virtual void Start()
        {
            Deselect();
            _currentHealthPoint = _unitData.HealthPoint;
        }

        public Rasa GetRasa() => _unitData.Rasa;

        public abstract void SetDamage(float damage);

        public void Select()
        {
            _selectionMark.gameObject.SetActive(true);
        }

        public void Deselect()
        {
            _selectionMark.gameObject.SetActive(false);
        }

        public List<Vector2> GetInteractablePositions()
        {
            return _interacteblePoints.GetPositionList();
        }
    }
}