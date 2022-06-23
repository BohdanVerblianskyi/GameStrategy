using System.Collections.Generic;
using _Project._ScriptsMain.Unit;
using _Project._ScriptsMain.Unit.MovebleUnit;
using UnityEngine;

namespace _Project._ScriptsMain.ExtractedResource
{
    public abstract class ExtractedResource : MonoBehaviour ,IInteractable,IDestroyable
    {
        [SerializeField] private List<Transform> _interacteblePoints;
        [SerializeField] protected int _minRecursesDrop;
        [SerializeField] protected int _maxRecursesDrop;
        protected abstract void ResourceDrop();

        public  List<Vector2> GetInteractablePositions()
        {
            return _interacteblePoints.GetPositionList();
        }

        public abstract void SetDamage(float damage);
    }
}